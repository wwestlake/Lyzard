using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.FileSystem
{
    public static class Paths
    {
        private static readonly string _default_base_directory
            = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Lyzard";

        private static string _baseDirectory;

        public static string BaseDirectory
        {
            get
            {
                return _baseDirectory ?? (_baseDirectory = _default_base_directory);
            }
            set
            {
                _baseDirectory = value;
            }
        }

        public static string ConfigDirectory { get { return CreatePath("Config"); } }
        public static string Data { get { return CreatePath("Data"); } }
        public static string Plugins { get { return CreatePath("Plugins"); } }
        public static string Projects { get { return CreatePath("Projects"); } }
        public static string Logs { get { return CreatePath("Logs"); } }

        public static string CreatePath(string basepath, string path)
        {
            return $"{basepath}\\{path}";
        }

        public static string CreatePath(string path)
        {
            return CreatePath(BaseDirectory, path);
        }



    }
}
