using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{ 
    public class CacheManager<T> where T: class
    {
        private static CacheManager<T> _instance;

        private Dictionary<Guid, MetaData<T>> _cache = new Dictionary<Guid, MetaData<T>>();



        public static CacheManager<T> Instance
        {
            get { return _instance ?? (_instance = new CacheManager<T>()); }
        }


        public IEnumerable<MetaData<T>> AddToCache(IEnumerable<T> items)
        {
            var result = new List<MetaData<T>>();
            foreach (var item in items)
            {
                result.Add(CheckCache(item));
            }
            return result;
        }


        public MetaData<T> RemoveFromCache(T item)
        {
            var meta = (MetaData<T>)item;
            if (meta == null) return null;
            if (_cache.ContainsKey(meta.Id))
            {
                _cache.Remove(meta.Id);
            }
            return meta;
        }

        public MetaData<T> CheckCache(T item)
        {
            var meta = (MetaData<T>)item;
            if (meta == null)
            {
                return NewMetaData(item);
            }
            else
            {
                return UpdateCache(meta);
            }
        }

        private MetaData<T> NewMetaData(T item)
        {
            var meta = MetaData<T>.Create(item);
            _cache.Add(meta.Id, meta);
            return meta;
        }

        private MetaData<T> UpdateCache(MetaData<T> meta)
        {
            if (_cache.ContainsKey(meta.Id))
            {
                var cacheMeta = _cache[meta.Id];
                cacheMeta.Modified = DateTime.Now;
                cacheMeta.Item = meta.Item;
                return cacheMeta;
            }
            else
            {
                _cache.Add(meta.Id, meta);
                return meta;
            }
        }

    }
}
