using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class SystemStorageManager<T> : StorageManager<T>
        where T : class
    {
        public SystemStorageManager()
        {
        }
    }
}
