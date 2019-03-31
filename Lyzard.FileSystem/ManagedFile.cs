using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.FileSystem
{
    public sealed class ManagedFile
    {
        public event FileSystemEventHandler Changed;


        private FileSystemSafeWatcher _watcher;

        public ManagedFile(string filepath)
        {
            if (!File.Exists(filepath)) File.Create(filepath).Close();
            FullPath = filepath;
            FilePath = Path.GetDirectoryName(filepath);
            FileName = Path.GetFileName(filepath);
            Extension = Path.GetExtension(FilePath);
            _watcher = new FileSystemSafeWatcher(FilePath, FileName);
            _watcher.Changed += OnChanged;
            _watcher.EnableRaisingEvents = true;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
        }


        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Changed?.Invoke(sender, e);
        }

        public string FilePath { get; }
        public string FileName { get; }
        public string Extension { get; }
        public string FullPath { get; }

        public string Load()
        {
            var reader = new StreamReader(File.OpenRead(FullPath));
            var result = reader.ReadToEnd();
            reader.Close();
            return result;
        }

        public void Save(string text)
        {
            _watcher.EnableRaisingEvents = false;
            var writer = new StreamWriter(File.Open(FullPath, FileMode.Truncate, FileAccess.Write));
            writer.Write(text);
            writer.Close();
            _watcher.EnableRaisingEvents = true;
        }
        
    }
}
