using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lyzard.FileSystem
{
    public class ManagedFolder
    {
        private string _path;
        private FileSystemSafeWatcher _watcher;

        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Deleted;
        public event FileSystemEventHandler Renamed;


        public ManagedFolder(string path)
        {
            if (! Directory.Exists(path)) Directory.CreateDirectory(path);
            _path = path;
            _watcher = new FileSystemSafeWatcher(_path, "*.*");
            _watcher.Changed += OnChanged;
            _watcher.Created += OnCreated;
            _watcher.Deleted += OnDeleted;
            _watcher.Renamed += OnRenamed;
            _watcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
        }

        public ManagedFile CreateFile(string filename)
        {
            return new ManagedFile($"{_path}\\filename");
        }

        public ManagedFolder CreateFolder(string foldername)
        {
            return new ManagedFolder($"{_path}\\foldername");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Changed?.Invoke(sender, e);
        }
        
        private void OnRenamed(object sender, System.IO.RenamedEventArgs e)
        {
            Renamed?.Invoke(sender, e);
        }

        private void OnDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            Deleted?.Invoke(sender, e);
        }

        private void OnCreated(object sender, System.IO.FileSystemEventArgs e)
        {
            Created?.Invoke(sender, e);
        }



    }
}
