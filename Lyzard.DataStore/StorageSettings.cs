using Lyzard.FileSystem;
using Lyzard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{

    public class StorageSettings : IStorageSettings
    {
        public StorageSettings()
        {
        }

        public string BaseLocation { get; set; }
 
        public Format Format { get ; set; }
        public ISerializer Serializer { get; set; }
    }
}
