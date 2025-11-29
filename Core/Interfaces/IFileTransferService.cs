using System;
using System.Threading;
using System.Threading.Tasks;
using ZastitaInformacija.Core.Algorithms.Crypto;
using ZastitaInformacija.Core.Algorithms.Hash;
using ZastitaInformacija.Core.Models;
using ZastitaInformacija.Core.Services;

namespace ZastitaInformacija.Core.Interfaces
{
    internal interface IFileTransferService
    {
        Task SendFileAsync(
            string host,
            int port,
            string filePath,
            ICryptoAlgorithm algorithm,
            TigerHash tiger,
            Action<string> logMessage,
            CancellationToken ct = default);

        Task StartListenerAsync(
            int port,
            Func<string?> selectOutputFolder,
            Func<CryptoAlgorithmId, ICryptoAlgorithm> algorithmFactory,
            Action<string> logMessage,
            CancellationToken ct = default);
    }
}