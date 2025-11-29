using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZastitaInformacija.Core.Algorithms.Crypto;
using ZastitaInformacija.Core.Interfaces;
using ZastitaInformacija.Core.Models;

namespace ZastitaInformacija.Core.Utilities
{
    internal class Mappings
    {
        public static CryptoAlgorithmId MapAlgorithmId(ICryptoAlgorithm alg) =>
           alg switch
           {
               Enigma => CryptoAlgorithmId.Enigma,
               XXTEA => CryptoAlgorithmId.XXTEA,
               XXTEACFB => CryptoAlgorithmId.XXTEACFB,
               _ => throw new NotSupportedException()
           };
        public static ICryptoAlgorithm MapAlgorithm(CryptoAlgorithmId algId) =>
           algId switch
           {
               CryptoAlgorithmId.Enigma => new Enigma(),
               CryptoAlgorithmId.XXTEA => new XXTEA(),
               CryptoAlgorithmId.XXTEACFB => new XXTEACFB(),
               _ => throw new NotSupportedException()
           };
    }
}
