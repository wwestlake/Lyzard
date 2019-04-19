using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class UserStorage<T> : StorageContract<T>
        where T : class
    {
        public UserStorage() : base(new UserStorageManager<T>())
        {
        }

        public UserStorage(IStorageContract<MetaWrapper<T, MetaData>> manager) : base(manager)
        {
        }
    }
}
