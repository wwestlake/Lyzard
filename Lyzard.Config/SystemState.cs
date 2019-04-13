using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Config
{
    public sealed class SystemState
    {
        private string _layout;
        private string _lastFileOpenLocation;

        public event EventHandler Changed;

        public SystemState()
        {
            RecentFiles = new List<string>();
            RecentProjects = new List<string>();

        }

        public IList<string> RecentFiles { get; private set; }

        public IList<string> RecentProjects { get; private set; }

        public string LastFileOpenLocation { get => _lastFileOpenLocation; set { _lastFileOpenLocation = value; OnChanged(); } }

        public string Layout
        {
            get { return _layout; }
            set
            {
                _layout = value;
                OnChanged();
            }
        }


        public void AddRecentFile(string file)
        {
            RecentFiles.Add(file);
            OnChanged();
        }

        public void AddRecentProject(string project)
        {
            RecentProjects.Add(project);
            OnChanged();
        }

        private void OnChanged()
        {
            Changed?.Invoke(this, new EventArgs());
        }

    }
}
