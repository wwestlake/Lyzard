using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.FileSystem
{
    public static class CommonFolders
    {
        public static readonly char Sep = Path.DirectorySeparatorChar;

        public static readonly string CompanyName = "LagDaemon";
        public static readonly string ProductName = "lyzard";
        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string Documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static readonly string LyzardAppData = AppData +  Sep + CompanyName + Sep + ProductName;
        public static readonly string LyzardDocs = Documents + Sep + CompanyName + Sep + ProductName;
        public static readonly string LyzardConfig =  LyzardAppData + Sep + "Config";
        public static readonly string LyzardPlugins = LyzardAppData + Sep + "Plugins";
        public static readonly string LyzardTemplates = LyzardAppData + Sep + "Templates";
        public static readonly string LyzardHelp = LyzardAppData + Sep + "Help";

        public static readonly string LyzardLayout = LyzardConfig + Sep + "Laout.xml";

        public static readonly string UserProjects = LyzardDocs + Sep + "Projects";
        public static readonly string UserTemplates = LyzardDocs + Sep + "Templates";
        public static readonly string UserPlugins = LyzardDocs + Sep + "Plugins";


        public static readonly string ProjectSource = "Source";
        public static readonly string ProjectOutput = "Output";
        public static readonly string ProjectConfig = "Config";

        private static readonly List<string> _folders = new List<string>()
        {
            LyzardAppData,
            LyzardDocs,
            LyzardConfig,
            LyzardPlugins,
            LyzardTemplates,
            LyzardHelp,
            UserProjects,
            UserTemplates,
            UserPlugins
        };


        static CommonFolders()
        {
            foreach (var folder in _folders)
            {
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            }
        }

    }
}
