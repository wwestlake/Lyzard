using Lyzard.FileSystem;
using Lyzard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public abstract class Container<T> : Storage<T> 
        where T : class
    {
        private string container;
        private ICacheManager cacheManager;

        public Container(string container) : this(container, CacheManager.Instance)
        {
        }

        protected Container(string container, ICacheManager cacheManager)
            : base(cacheManager)
        {
            ContainerName = container;
        }

        public string ContainerName { get; }

        protected override string BasePath
        {
            get
            {
                var sep = CommonFolders.Sep;
                return $"{Settings.BaseLocation}{sep}{ContainerName}{sep}";
            }
            set { }
        }

        protected override string IndexFile { get; set; }

    }
}
