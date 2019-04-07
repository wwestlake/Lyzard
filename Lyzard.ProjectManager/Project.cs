using Lyzard.FileSystem;
using Lyzard.Interfaces;
using Lyzard.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.ProjectManager
{
    public class Project
    {

        private string _baseDirectory;

        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string Company { get; set; }

        public Version Version { get; set; } = new Version("1.0.0.0");

        public string Copyright { get; set; }
        public Language Language { get; set; } = Language.CSharp;

        public string Description { get; set; }

        public ObservableCollection<AssemblyName> References { get; set; } = new ObservableCollection<AssemblyName>();

        public ObservableCollection<string> IncludedFiles { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<Project> SubProjects { get; set; } = new ObservableCollection<Project>();

        [JsonIgnore]
        public Project Parent { get; set; }

        public Guid ParentId { get; set; }

        [JsonIgnore]
        public string BaseDirectory
        {
            get
            {
                if (Parent == null)
                {
                    return _baseDirectory;
                }
                else
                {
                    return Parent.BaseDirectory + CommonFolders.Sep + Name;
                }
            }
            set
            {
                if (Parent == null)
                {
                    _baseDirectory = value;
                }
            }
        }

        [JsonIgnore]
        public ManagedFile File { get; set; }

        public void AddProject(Project project)
        {
            if (SubProjects.Contains(project)) return;
            project.ParentId = Guid;
            project.Parent = this;

            if (!Directory.Exists(project.BaseDirectory)) Directory.CreateDirectory(project.BaseDirectory);

            SubProjects.Add(project);
        }

        public void RemoveProject(Project project)
        {
            if (!SubProjects.Contains(project)) return;
            SubProjects.Remove(project);
        }

        public void AddReferencet(AssemblyName assembly)
        {
            if (References.Contains(assembly)) return;
            References.Add(assembly);
        }

        public void RemoveAssembly(AssemblyName assembly)
        {
            if (!References.Contains(assembly)) return;
            References.Remove(assembly);
        }

        public void AddIncludedFile(string filepath)
        {
            var relativePath = BaseDirectory.MakeRelativePath(filepath);

            if (IncludedFiles.Contains(relativePath)) return;
            IncludedFiles.Add(relativePath);
        }

        public void RemoveIncludedFile(string filename)
        {
            if (!IncludedFiles.Contains(filename)) return;
            IncludedFiles.Remove(filename);
        }

        public void Serialize(ManagedFile file)
        {
            File = file;
            Serialize();
        }

        public void Serialize()
        {
            if (File == null) throw new ArgumentNullException($"Project File must be set: Project: {Name}");
            File.Save(JsonConvert.SerializeObject(this, Formatting.Indented));

        }

        public static Project Deserialize(ManagedFile file)
        {
            var text = file.Load();
            var result = JsonConvert.DeserializeObject<Project>(text);
            result.BaseDirectory = file.FilePath;
            SetParentage(result);
            return result;
        }

        private static void SetParentage(Project project)
        {
            foreach (var proj in project.SubProjects)
            {
                proj.Parent = project;
                SetParentage(proj);
            }
        }
    }
}
