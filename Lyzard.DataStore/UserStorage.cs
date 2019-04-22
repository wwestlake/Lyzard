using Lyzard.FileSystem;
using Lyzard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class UserStorage<T> : Container<T>
        where T: class
    {

        public UserStorage(string container) : this(container, CacheManager.Instance)
        {
        }

        public UserStorage(string container, ICacheManager cacheManager)
            : base(container, cacheManager)
        {
            Settings.BaseLocation = CommonFolders.UserDataStore;
            var sep = CommonFolders.Sep;
            IndexFile = $"{BasePath}{sep}{container}.idx";
            DataFile = $"{BasePath}{sep}{CheckIndex().ToString()}.dat";
            CheckIndex();
        }

        protected override string DataFile { get; set; }
    }
}
