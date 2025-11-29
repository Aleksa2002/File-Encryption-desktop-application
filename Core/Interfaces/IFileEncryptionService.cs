using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaInformacija.Core.Interfaces
{
    internal interface IFileEncryptionService
    {
        void EncryptFile(string filePath, string outputFolderPath, ICryptoAlgorithm algorithm, Action<string> logMessage);
        void DecryptFile(string filePath, Func<string?> selectOutputFolder, Action<string> logMessage);
    }
}
