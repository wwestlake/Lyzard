using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class StorageManager<T> : IStorageContract<MetaData<T>>
        where T : class
    {

        public StorageManager()
        {

        }

        public IStorageSettings Settings { get; set; }


        public void Delete(MetaData<T> item)
        {
            throw new NotImplementedException();
        }

        public MetaData<T> Find(Predicate<MetaData<T>> predicate)
        {
            throw new NotImplementedException();
        }

        public MetaData<T> Find(Predicate<MetaData<T>> predicate, int revision)
        {
            throw new NotImplementedException();
        }

        public Guid? Identify(MetaData<T> item)
        {
            throw new NotImplementedException();
        }

        public void Prune(MetaData<T> item)
        {
            throw new NotImplementedException();
        }

        public void Prune(MetaData<T> item, int revision)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MetaData<T>> Query(Predicate<MetaData<T>> predicate)
        {
            throw new NotImplementedException();
        }

        public MetaData<T> Store(MetaData<T> item)
        {
            throw new NotImplementedException();
        }
    }
}
