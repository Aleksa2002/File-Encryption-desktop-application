using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZastitaInformacija.Core.Algorithms.Crypto;
using ZastitaInformacija.Core.Algorithms.Hash;
using ZastitaInformacija.Core.Interfaces;
using ZastitaInformacija.Core.Models;
using ZastitaInformacija.Core.Utilities;

namespace ZastitaInformacija.Core.Services
{
    internal class FileTransferService: IFileTransferService
    {
        // Message layout (in order):
        // 1. Diffie-Hellman public key length (Int32) + public key bytes (one-time for connection)
        // 2. (Response side sends its public key length + key)
        // For each file:
        // 3. Algorithm Id (byte)
        // 4. File name (BinaryWriter.Write(string) => length-prefixed)
        // 5. Original file size (Int64)
        // 6. Hash length (Int32)
        // 7. Hash bytes
        // 8. Encrypted file content (raw)  (Spec does not include explicit length – assumes encryption keeps size)
        // NOTE: If algorithm changes file size (padding), prepend an Int64 encryptedSize after hash to be robust.

        public async Task SendFileAsync(
            string host,
            int port,
            string filePath,
            ICryptoAlgorithm algorithm,
            TigerHash tiger,
            Action<string> logMessage,
            CancellationToken ct = default)
        {
            using var client = new TcpClient();
            try
            {
                await client.ConnectAsync(host, port, ct).ConfigureAwait(false);
            }
            catch
            {
                logMessage($"❌ Couldn't connect to target machine on IP address: {host}:{port}.");
                logMessage("❌ Target machine must enable ✅ Port listening option inside app settings ⚙.");
                return;
            }
            using var network = client.GetStream();
            using var writer = new BinaryWriter(network, Encoding.UTF8, leaveOpen: true);
            using var reader = new BinaryReader(network, Encoding.UTF8, leaveOpen: true);

            // 1. Perform Diffie-Hellman (ECDH over P-256)
            byte[] sharedSecret = PerformKeyExchangeAsInitiator(writer, reader);

            byte[] key = algorithm.GenerateRandomKey(sharedSecret);

            // 2. Gather file + encrypt
            string fileName = Path.GetFileName(filePath);
            byte[] plain = File.ReadAllBytes(filePath);
            byte[] encrypted = algorithm.Encrypt(plain, key);
            byte[] hash = tiger.ComputeHash(encrypted);

            // 3. Send metadata + data
            writer.Write((byte)Mappings.MapAlgorithmId(algorithm));
            writer.Write(fileName);
            writer.Write((long)plain.Length);
            writer.Write(hash.Length);
            writer.Write(hash);

            // Spec assumes encrypted size == original size; for padding algos you may want to send encrypted.Length explicitly.
            writer.Write(encrypted);
            writer.Flush();

            logMessage($"✔ File sent successfully 📄.");
            logMessage($"✔ To ip address 📡 💻: {host}:{port}");
        }

        public async Task StartListenerAsync(
            int port,
            Func<string?> selectOutputFolder,
            Func<CryptoAlgorithmId, ICryptoAlgorithm> algorithmFactory,
            Action<string> logMessage,
            CancellationToken ct = default)
        {
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            logMessage($"Port listening started 🚀 📈.");
            logMessage($"Started listening on port 👂 🔌: {port}");

            try
            {
                while (!ct.IsCancellationRequested)
                {
                    if (!listener.Pending())
                    {
                        await Task.Delay(100, ct).ConfigureAwait(false);
                        continue;
                    }

                    _ = Task.Run(async () =>
                    {
                        using var client = await listener.AcceptTcpClientAsync(ct).ConfigureAwait(false);
                        using var network = client.GetStream();
                        using var writer = new BinaryWriter(network, Encoding.UTF8, leaveOpen: true);
                        using var reader = new BinaryReader(network, Encoding.UTF8, leaveOpen: true);

                        try
                        {
                            // DH as responder
                            byte[] sharedSecret = PerformKeyExchangeAsResponder(reader, writer);

                            // Per-file receive loop (close after one file for simplicity)
                            CryptoAlgorithmId algId = (CryptoAlgorithmId)reader.ReadByte();
                            ICryptoAlgorithm algorithm = algorithmFactory(algId);
                            byte[] key = algorithm.GenerateRandomKey(sharedSecret);

                            string fileName = reader.ReadString();
                            long originalSize = reader.ReadInt64();
                            int hashLen = reader.ReadInt32();
                            byte[] expectedHash = reader.ReadBytes(hashLen);

                            // Read remaining stream into buffer (single file assumption)
                            using var ms = new MemoryStream();
                            network.CopyTo(ms);
                            byte[] encrypted = ms.ToArray();

                            byte[] decrypted = algorithm.Decrypt(encrypted, key);

                            // Verify hash
                            var tiger = new TigerHash();
                            byte[] actualHash = tiger.ComputeHash(encrypted);
                            bool ok = CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);

                            var selectedFolderPath = selectOutputFolder();
                            if (!Directory.Exists(selectedFolderPath))
                            {
                                logMessage("❌ No selected folder to save the decrypted file.");
                                return;
                            }
                            string savePath = Path.Combine(selectedFolderPath, fileName);

                            if (ok)
                            {
                                File.WriteAllBytes(savePath, decrypted);
                                logMessage("✔ File received and verified successfully.");
                                logMessage($"✔ File saved to 📄: {savePath}.");
                            }
                            else
                            {
                                logMessage($"❌ Hash mismatch for file 📄: {fileName}.");
                                logMessage("❌ File discarded 🗑.");

                            }

                        }
                        catch (Exception ex)
                        {
                            logMessage($"❌ Receiver error: {ex.Message}.");
                        }
                    }, ct);
                }
            }
            catch (TaskCanceledException)
            {
                logMessage($"Port listening stopped ⏸ 📉.");
            }
            finally
            {
                listener.Stop();
            }
        }

        private static byte[] PerformKeyExchangeAsInitiator(BinaryWriter writer, BinaryReader reader)
        {
            using var ecdh = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP256);
            byte[] pub = ecdh.PublicKey.ExportSubjectPublicKeyInfo();
            writer.Write(pub.Length);
            writer.Write(pub);
            writer.Flush();

            int otherLen = reader.ReadInt32();
            byte[] otherPub = reader.ReadBytes(otherLen);
            using var otherKey = ECDiffieHellman.Create();
            otherKey.ImportSubjectPublicKeyInfo(otherPub, out _);

            return ecdh.DeriveKeyMaterial(otherKey.PublicKey);
        }

        private static byte[] PerformKeyExchangeAsResponder(BinaryReader reader, BinaryWriter writer)
        {
            using var ecdh = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP256);

            int otherLen = reader.ReadInt32();
            byte[] otherPub = reader.ReadBytes(otherLen);

            byte[] pub = ecdh.PublicKey.ExportSubjectPublicKeyInfo();
            writer.Write(pub.Length);
            writer.Write(pub);
            writer.Flush();

            using var otherKey = ECDiffieHellman.Create();
            otherKey.ImportSubjectPublicKeyInfo(otherPub, out _);
            return ecdh.DeriveKeyMaterial(otherKey.PublicKey);
        }
    }
}