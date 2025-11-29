using System;

namespace ZastitaInformacija.Core.Interfaces
{
    internal interface IFileWatcherService
    {
        event Action<string> FileCreated;
        void SetWatcherPath(string folderPath);
        void SetIsWatcherEnabled(bool isWatching);
    }
}