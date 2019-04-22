using Lyzard.FileSystem;
using Lyzard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class SystemStorage<T> : Container<T>
        where T: class
    {
        private Guid id;
        private string container;

        public SystemStorage(string container) : this(container, CacheManager.Instance)
        {
        }

        public SystemStorage(string container, ICacheManager cacheManager)
            : base(container)
        {
            Settings.BaseLocation = CommonFolders.LyzardDataStore;
            var sep = CommonFolders.Sep;
            IndexFile = $"{BasePath}{sep}{container}.idx";
            DataFile = $"{BasePath}{sep}{CheckIndex().ToString()}.dat";
            CheckIndex();
        }

        protected override string DataFile { get; set; }


        

    }
}
