using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaInformacija.Core.Interfaces
{
    internal interface ICryptoAlgorithm
    {
        byte[] Encrypt(byte[] data, byte[] key);
        byte[] Decrypt(byte[] data, byte[] key);
        byte[] GenerateRandomKey(byte[]? seed = null);
    }
}
