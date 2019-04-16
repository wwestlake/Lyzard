using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public sealed class SystemStorage : StorageContract
    {
        private static SystemStorage _instance;


        private SystemStorage(string container) : base()
        {
            Settings.BaseLocation = CommonFolders.LyzardDataStore + CommonFolders.Sep + container;
            Settings.IndexFile = Settings.BaseLocation + CommonFolders.Sep + "Index.json";
            LoadIndex();
        }

        public static IStorageContract Create(string container)
        {
            return _instance ?? (_instance = new SystemStorage(container));
        }

    }
}
