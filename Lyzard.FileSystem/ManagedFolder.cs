/* 
 * Lyzard Code Generation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
