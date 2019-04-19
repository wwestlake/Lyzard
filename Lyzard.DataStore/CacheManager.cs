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

        private Dictionary<Guid, MetaWrapper<T, MetaData>> _cache = new Dictionary<Guid, MetaWrapper<T, MetaData>>();



        public static CacheManager<T> Instance
        {
            get { return _instance ?? (_instance = new CacheManager<T>()); }
        }


        public IEnumerable<MetaWrapper<T, MetaData>> AddToCache(IEnumerable<T> items)
        {
            var result = new List<MetaWrapper<T, MetaData>>();
            foreach (var item in items)
            {
                result.Add(CheckCache(item));
            }
            return result;
        }


        public MetaWrapper<T, MetaData> RemoveFromCache(T item)
        {
            var meta = (MetaWrapper<T, MetaData>)item;
            if (meta == null) return null;
            if (_cache.ContainsKey(meta.Meta.Id))
            {
                _cache.Remove(meta.Meta.Id);
            }
            return meta;
        }

        public MetaWrapper<T, MetaData> CheckCache(T item)
        {
            var meta = (MetaWrapper<T, MetaData>)item;
            if (meta == null)
            {
                return NewMetaData(item);
            }
            else
            {
                return UpdateCache(meta);
            }
        }

        private MetaWrapper<T, MetaData> NewMetaData(T item)
        {
            var meta = (MetaWrapper<T, MetaData>) MetaWrapper<T, MetaData>.Create(item);
            _cache.Add(meta.Meta.Id, meta);
            return meta;
        }

        private MetaWrapper<T, MetaData> UpdateCache(MetaWrapper<T, MetaData> meta)
        {
            if (_cache.ContainsKey(meta.Meta.Id))
            {
                var cacheMeta = _cache[meta.Meta.Id];
                cacheMeta.Meta.Modified = DateTime.Now;
                return cacheMeta;
            }
            else
            {
                _cache.Add(meta.Meta.Id, meta);
                return meta;
            }
        }

    }
}
