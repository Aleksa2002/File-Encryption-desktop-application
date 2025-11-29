using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZastitaInformacija.Core.Interfaces;
using ZastitaInformacija.Core.Models;
using ZastitaInformacija.Core.Settings;
using ZastitaInformacija.Core.Utilities;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ZastitaInformacija.Core.Services
{
    internal class FileEncryptionService : IFileEncryptionService
    {
        private readonly string _keyFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public void EncryptFile(string filePath, string outputFolderPath, ICryptoAlgorithm algorithm, Action<string> logMessage)
        {
            var data = File.ReadAllBytes(filePath);
            var key = algorithm.GenerateRandomKey();
            var encrypted = algorithm.Encrypt(data, key);
            var keyId = Guid.NewGuid().ToString("N"); // Generate a 16-character key ID
            var keyIdBytes = Encoding.UTF8.GetBytes(keyId); // Convert to bytes
            var keyFileName = Path.Combine(_keyFolderPath, $"key_{keyId}.key");
            File.WriteAllBytes(keyFileName, [(byte)Mappings.MapAlgorithmId(algorithm)]);
            File.AppendAllBytes(keyFileName, key); // Save the key to a file
            var outputFileName = Path.Combine(outputFolderPath,
                                            $"{Path.GetFileNameWithoutExtension(filePath)}_encrypted{Path.GetExtension(filePath)}");
            File.WriteAllBytes(outputFileName, keyIdBytes);
            File.AppendAllBytes(outputFileName, encrypted); // Save the encrypted data to a file
            logMessage("✔ File encrypted successfully.");
            logMessage($"✔ File saved to 📄: {outputFileName}.");
            logMessage($"✔ Key saved to 🗝: {keyFileName}.");
                            
        }
        public void DecryptFile(string filePath, Func<string?> selectOutputFolder, Action<string> logMessage)
        {
            var fileBytes = File.ReadAllBytes(filePath);
            var keyIdLength = 32; // Key ID length in bytes
            if (fileBytes.Length < keyIdLength)
            {
                logMessage("❌ File does not appear to be encrypted (missing key header).");
                return;
            }
            byte[] keyIdBytes = new byte[keyIdLength];
            Buffer.BlockCopy(fileBytes, 0, keyIdBytes, 0, keyIdLength);
            byte[] data = new byte[fileBytes.Length - keyIdLength];
            Buffer.BlockCopy(fileBytes, keyIdLength, data, 0, fileBytes.Length - keyIdLength);
            var keyId = Encoding.UTF8.GetString(keyIdBytes);
            var keyFileName = Path.Combine(_keyFolderPath, $"key_{keyId}.key");
            if (!File.Exists(keyFileName))
            {
                logMessage("❌ Failed to find the key for this file.");
                return;
            }
            var key = File.ReadAllBytes(keyFileName);
            var algorithmId = (CryptoAlgorithmId)key[0]; // First byte is the algorithm ID
            key = [.. key.Skip(1)]; // Remove the algorithm ID byte
            var decrypted = Mappings.MapAlgorithm(algorithmId).Decrypt(data, key);

            var selectedFolderPath = selectOutputFolder();
            if (!Directory.Exists(selectedFolderPath))
            {
                logMessage("❌ Failed find selected folder to save the decrypted file.");
                return;
            }
            var outputFileName = Path.Combine(selectedFolderPath,
                                            $"{Path.GetFileNameWithoutExtension(filePath)}_decrypted{Path.GetExtension(filePath)}");
            File.WriteAllBytes(outputFileName, decrypted);
            logMessage("✔ File decrypted successfully.");
            logMessage($"✔ File saved to 📄: {outputFileName}.");
        }

    }
}
