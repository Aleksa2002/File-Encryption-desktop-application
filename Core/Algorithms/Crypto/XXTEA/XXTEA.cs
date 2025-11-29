using System.Diagnostics;
using System.Security.Cryptography;
using ZastitaInformacija.Core.Interfaces;

namespace ZastitaInformacija.Core.Algorithms.Crypto
{
    public class XXTEA : ICryptoAlgorithm
    {
        private const uint DELTA = 0x9e3779b9;

        public byte[] GenerateRandomKey(byte[]? seed = null)
        {
            var result = new byte[16]; // XXTEA key must be 16 bytes
            if (seed is null)
            {
                RandomNumberGenerator.Fill(result);
            }
            else
            {
                // Deterministic: hash seed and take first 16 bytes
                var hash = SHA256.HashData(seed);
                Buffer.BlockCopy(hash, 0, result, 0, 16);
            }
            return result;
        }

        public byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data.Length == 0) return data;

            var paddedData = AddPadding(data, 4);
            uint[] v = ToUInt32Array(paddedData);
            uint[] k = ToUInt32Array(FixKey(key));

            Btea(v, k, true);
            return ToByteArray(v);
        }

        public byte[] Decrypt(byte[] data, byte[] key)
        {
            if (data.Length == 0) return data;

            uint[] v = ToUInt32Array(data);
            uint[] k = ToUInt32Array(FixKey(key));

            Btea(v, k, false);

            var result = ToByteArray(v);
            // Remove padding
            return RemovePadding(result, 4);
        }

        public static byte[] EncryptBlock(byte[] block, byte[] key, int blockSize)
        {
            if (block.Length != blockSize)
                throw new ArgumentException("Block must be exactly 16 bytes.");

            uint[] v = ToUInt32Array(block);
            uint[] k = ToUInt32Array(FixKey(key));
            Btea(v, k, true);
            return ToByteArray(v);
        }

        private static void Btea(uint[] v, uint[] k, bool encrypt)
        {
            uint y, z, sum;
            uint p, rounds, e;
            int n = v.Length;

            if (encrypt) // Encryption
            {
                rounds = (uint)(6 + 52 / n);
                sum = 0;
                z = v[n - 1];

                do
                {
                    sum += DELTA;
                    e = sum >> 2 & 3;
                    for (p = 0; p < n - 1; p++)
                    {
                        y = v[p + 1];
                        v[p] += MX(z, y, sum, k, p, e);
                        z = v[p];
                    }
                    y = v[0];
                    v[n - 1] += MX(z, y, sum, k, p, e);
                    z = v[n - 1];
                } while (--rounds > 0);
            }
            else // Decryption
            {
                rounds = (uint)(6 + 52 / n);
                sum = rounds * DELTA;
                y = v[0];

                do
                {
                    e = sum >> 2 & 3;
                    for (p = (uint)(n - 1); p > 0; p--)
                    {
                        z = v[p - 1];
                        v[p] -= MX(z, y, sum, k, p, e);
                        y = v[p];
                    }
                    z = v[n - 1];
                    v[0] -= MX(z, y, sum, k, p, e);
                    y = v[0];
                    sum -= DELTA;
                } while (--rounds > 0);
            }
        }

        private static uint MX(uint z, uint y, uint sum, uint[] k, uint p, uint e)
        {
            return (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z);
        }

        private static uint[] ToUInt32Array(byte[] data)
        {

            var result = new uint[data.Length / 4];

            for (int i = 0; i < data.Length / 4; i++)
                result[i] = BitConverter.ToUInt32(data, i * 4);

            return result;
        }

        private static byte[] ToByteArray(uint[] data)
        {

            byte[] result = new byte[data.Length * 4];
            for (int i = 0; i < data.Length; i++)
            {
                var bytes = BitConverter.GetBytes(data[i]);
                Buffer.BlockCopy(bytes, 0, result, i * 4, 4);
            }
            return result;
        }

        private static byte[] FixKey(byte[] key)
        {
            byte[] fixedKey = new byte[16]; // XXTEA key must be 16 bytes
            Buffer.BlockCopy(key, 0, fixedKey, 0, Math.Min(key.Length, 16));
            return fixedKey;
        }

        private static byte[] AddPadding(byte[] data, int blockSize)
        {
            int padding = blockSize - data.Length % blockSize;
            byte[] padded = new byte[data.Length + padding];
            Buffer.BlockCopy(data, 0, padded, 0, data.Length);

            // PKCS#7 padding – svaki bajt = broj padding bajtova
            for (int i = data.Length; i < padded.Length; i++)
                padded[i] = (byte)padding;

            return padded;
        }

        private static byte[] RemovePadding(byte[] data, int blockSize)
        {
            if (data.Length == 0 || data.Length % blockSize != 0)
                throw new ArgumentException("Invalid padded data.");

            int padding = data[^1];
            if (padding < 1 || padding > blockSize)
                throw new ArgumentException("Invalid PKCS7 padding.");

            for (int i = data.Length - padding; i < data.Length; i++)
            {
                if (data[i] != padding)
                    throw new ArgumentException("Invalid PKCS7 padding.");
            }

            byte[] unpadded = new byte[data.Length - padding];
            Buffer.BlockCopy(data, 0, unpadded, 0, unpadded.Length);
            return unpadded;
        }
    }
}
