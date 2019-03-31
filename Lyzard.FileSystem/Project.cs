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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.FileSystem
{
    public class Project
    {
        private ManagedFile _file;
        private ManagedFolder _folder;
        private readonly List<ManagedFile> _managedFiles = new List<ManagedFile>();

        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Deleted;
        public event FileSystemEventHandler Renamed;

        private Project()
        {
        }


        private void OnRenamed(object sender, FileSystemEventArgs e)
        {
            Renamed?.Invoke(sender, e);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Deleted?.Invoke(sender, e);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Created?.Invoke(sender, e);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Changed?.Invoke(sender, e);
        }


        public string ProjectDirectory { get; set; }
        public string ProjectName { get; set; }
        public string Config { get; set; }
        public string Output { get; set; }

        public string ProjectFileName { get; set; }

        public string Company { get; set; }
        public string Copyright { get; set; }
        public Version Version { get; set; }
        public Guid Guid { get; set; }
        public string Product { get; set; }

        public IList<string> IncludedFiles { get; } = new List<string>();
        public IList<string> IncludedFolders { get; } = new List<string>();

        public IList<string> References { get; } = new List<string>();

        public string Contact { get; set; }
        public string AssemblyName { get; set; }

        public void CreateFile(string filename)
        {
            IncludedFiles.Add(filename);
            var file = new ManagedFile($"{ProjectDirectory}\\{filename}");
            file.Changed += OnChanged;
            _managedFiles.Add(file);
        }

        public void CreateFolder(string folder)
        {
            IncludedFolders.Add(folder);
            Directory.CreateDirectory($"{ProjectDirectory}\\{folder}");
        }

        public static Project Load(string path)
        {
            if (!File.Exists(path)) return null;

            var text = File.ReadAllText(path);

            var project = JsonConvert.DeserializeObject<Project>(text);
            project.HookUpEvents();
            return project;
        }

        public void Save()
        {
            var text = JsonConvert.SerializeObject(this, Formatting.Indented);
            _file.Save(text);
        }

        

        public static Project Create(string projectDirectory, string projectName)
        {
            var project = new Project();
            project.Initialize(projectDirectory, projectName);
            project.HookUpEvents();
            project.Save();
            return project;
        }



        private void Initialize(string projectDirectory, string projectName)
        {
            ProjectDirectory = projectDirectory;
            ProjectName = projectName;
            Config = $"{ProjectDirectory}\\{ProjectName}\\.lyzard";
            Output = $"{ProjectDirectory}\\{ProjectName}\\bin";
            ProjectFileName = $"{ProjectDirectory}\\{ProjectName}\\{ProjectName}.lyzard";
            Version = new Version(1, 0, 0, 0);
            AssemblyName = ProjectName;
            Guid = Guid.NewGuid();
            Product = ProjectName;
            CreateDirectories();
            HookUpEvents();
        }


        private void CreateDirectories()
        {
            if (!Directory.Exists(Config)) Directory.CreateDirectory(Config);
            if (!Directory.Exists(Output)) Directory.CreateDirectory(Output);

        }

        private void HookUpEvents()
        {
            _file = new ManagedFile(ProjectFileName);
            _file.Changed += OnChanged;
            _folder = new ManagedFolder(ProjectDirectory);
            _folder.Changed += OnChanged;
            _folder.Created += OnCreated;
            _folder.Deleted += OnDeleted;
            _folder.Renamed += OnRenamed;
        }

    }
}
