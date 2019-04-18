using Lyzard.FileSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    /// <summary>
    /// Contract for all storage classes
    /// </summary>
    public abstract class StorageContract<T> : IStorageContract<T>
        where T: class
    {
        private readonly IStorageContract<MetaData<T>> _manager;

        public StorageContract(IStorageContract<MetaData<T>> storageManager)
        {
            _manager = storageManager;
        }

        public void Delete(T item)
        {
            var meta = RemoveFromCache(item);
            _manager.Delete(meta);
        }

        public T Find(Predicate<T> predicate)
        {
            return _manager.Find((entity) => predicate(entity.Item));
        }

        public T Find(Predicate<T> predicate, int revision)
        {
            return _manager.Find((entity) => predicate(entity.Item), revision);
        }

        public Guid? Identify(T item)
        {
            return _manager.Identify(item);
        }

        public void Prune(T item)
        {
            _manager.Prune(item);
        }

        public void Prune(T item, int revision)
        {
            _manager.Prune(item, revision);
        }

        public IEnumerable<T> Query(Predicate<T> predicate)
        {
            return (IEnumerable<T>)AddToCache( _manager.Query((entity) => predicate(entity.Item)) );
        }


        public T Store(T item)
        {
            return _manager.Store(CheckCache(item));
        }


        /***********************************************************/

        private MetaData<T> CheckCache(T item)
        {
            return CacheManager<T>.Instance.CheckCache(item);
        }

        private MetaData<T> RemoveFromCache(T item)
        {
            return CacheManager<T>.Instance.RemoveFromCache(item);
        }

        public IEnumerable<MetaData<T>> AddToCache(IEnumerable<T> items)
        {
            return CacheManager<T>.Instance.AddToCache(items);
        }
        private IEnumerable<T> AddToCache(IEnumerable<MetaData<T>> items)
        {
            var _items = items.Select(item => (T)item).AsEnumerable<T>();
            var result = AddToCache(_items).Select(item => (T)item).AsEnumerable<T>();
            return result;
        }

    }
}
