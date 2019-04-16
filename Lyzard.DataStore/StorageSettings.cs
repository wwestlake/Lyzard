using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{

    public class StorageSettings : IStorageSettings
    {
        public string BaseLocation { get; set; }
        public string IndexFile
        {
            get
            {
                return BaseLocation + CommonFolders.Sep + "Index.json";
            }
        }

        public string Container { get; set; }
        string IStorageSettings.IndexFile { get; set; }
    }
}
