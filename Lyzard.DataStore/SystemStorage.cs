using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public sealed class SystemStorage<T> : StorageContract<T>
        where T : class
    {
        public SystemStorage() : base(new SystemStorageManager<T>())
        {
        }

        public SystemStorage(IStorageContract<MetaWrapper<T, MetaData>> manager) : base(manager) { }
    }
}
