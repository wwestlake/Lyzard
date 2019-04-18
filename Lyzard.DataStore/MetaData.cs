using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public sealed class MetaData<T> where T: class
    {
        private static Dictionary<T, MetaData<T>> _stash = new Dictionary<T, MetaData<T>>();

        public T Item { get; internal set; }
        public Guid Id { get; internal set; }
        public DateTime Created { get; internal set; }
        public DateTime Modified { get; internal set; }
        public int Revision { get; internal set; }


        private MetaData()
        {

        }

        public static implicit operator T(MetaData<T> meta)
        {
            if (meta == null) return null;
            if (!_stash.ContainsKey(meta.Item))
            {
                _stash.Add(meta.Item, meta);
            }
            return meta.Item;
        }

        public static implicit operator MetaData<T>(T item)
        {
            if (item == null) return null;
            if (_stash.ContainsKey(item))
            {
                return _stash[item];
            }
            return null;
        }

        public static MetaData<T> Create(T item)
        {
            if (_stash.ContainsKey(item))
            {
                return _stash[item];
            }
            return new MetaData<T> {
                Id = Guid.NewGuid(),
                Item = item,
                Revision = 0,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };
        }

        public override string ToString()
        {
            return Item.ToString();
        }
    }
}
