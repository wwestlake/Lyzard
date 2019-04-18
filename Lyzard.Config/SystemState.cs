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
        public SystemState()
        {
            RecentFiles = new List<string>();
            RecentProjects = new List<string>();
        }

        public IList<string> RecentFiles { get; private set; }

        public IList<string> RecentProjects { get; private set; }

        public string LastFileOpenLocation { get; set; }

        public string Layout { get; set; }

        public double CommandConsoleFontSize { get; set; } = 16;
        public double PowerShellConsoleFontSize { get; set; } = 16;

        public double OutputConsoleFontSize { get; set; } = 16;



    }
}
