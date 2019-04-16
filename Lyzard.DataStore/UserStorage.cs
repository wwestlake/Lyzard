using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class UserStorage : StorageContract
    {
        private static UserStorage _instance;


        private UserStorage(string container) : base()
        {
            Settings.BaseLocation = CommonFolders.UserDataStore + CommonFolders.Sep + container;
        }

        public static IStorageContract Create(string container)
        {
            return _instance ?? (_instance = new UserStorage(container));
        }
    }
}
