using System;
using System.IO;
using ZastitaInformacija.Core.Interfaces;

namespace ZastitaInformacija.Core.Services
{
    public class FileWatcherService : IFileWatcherService
    {
        private FileSystemWatcher? _watcher;

        public event Action<string> FileCreated;

        public FileWatcherService(Action<string> onFileCreated)
        {
            FileCreated += onFileCreated;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            // Optionally, add logic to ensure the file is fully written before processing
            FileCreated?.Invoke(e.FullPath);
        }

        public void SetWatcherPath(string folderPath)
        {
            if (_watcher == null)
            {
                _watcher = new FileSystemWatcher(folderPath)
                {
                    NotifyFilter = NotifyFilters.FileName,
                    EnableRaisingEvents = false,
                    IncludeSubdirectories = false
                };
                _watcher.Created += OnCreated;
            }
            else if (!folderPath.Equals(_watcher.Path))
            {
                _watcher.EnableRaisingEvents = false; // Disable events while changing path
                _watcher.Path = folderPath;
            }

        }
        public void SetIsWatcherEnabled(bool isWatching)
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = isWatching;
            }
        }
    }
}