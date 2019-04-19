using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class StorageManager<T> : IStorageContract<MetaWrapper<T, MetaData>>
        where T : class
    {

        public StorageManager()
        {

        }

        public IStorageSettings Settings { get; set; }


        public void Delete(MetaWrapper<T, MetaData> item)
        {
            throw new NotImplementedException();
        }

        public MetaWrapper<T, MetaData> Find(Predicate<MetaWrapper<T, MetaData>> predicate)
        {
            throw new NotImplementedException();
        }

        public MetaWrapper<T, MetaData> Find(Predicate<MetaWrapper<T, MetaData>> predicate, int revision)
        {
            throw new NotImplementedException();
        }

        public Guid? Identify(MetaWrapper<T, MetaData> item)
        {
            throw new NotImplementedException();
        }

        public void Prune(MetaWrapper<T, MetaData> item)
        {
            throw new NotImplementedException();
        }

        public void Prune(MetaWrapper<T, MetaData> item, int revision)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MetaWrapper<T, MetaData>> Query(Predicate<MetaWrapper<T, MetaData>> predicate)
        {
            throw new NotImplementedException();
        }

        public MetaWrapper<T, MetaData> Store(MetaWrapper<T, MetaData> item)
        {
            throw new NotImplementedException();
        }
    }
}
