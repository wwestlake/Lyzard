/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Lyzard.Collections;
using Lyzard.Interfaces;
using Lyzard.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public abstract class Storage<T> : IStorageContract<T>
        where T : class
    {
        private ICacheManager _cache = CacheManager.Instance;
        protected Dictionary<Type, Guid> _index;
        protected List<MetaWrapper<T, MetaData>> _data;
        protected bool _isValid = true;

        public IStorageSettings Settings { get; set; } = new StorageSettings();

        public Storage() : this(CacheManager.Instance)
        {
        }

        protected Storage(ICacheManager cacheManager)
        {
            this._cache = cacheManager;
            Settings.Serializer = new JsonSerializer();
            Settings.Format = Format.Indented;
        }

        public void Purge()
        {
            CheckIndex();
            _data = _cache.Read<List<MetaWrapper<T, MetaData>>>(DataFile);
            _cache.Delete(DataFile);
            _index.Remove(typeof(T));
            WriteIndex();
        }

        public void Delete(T item)
        {
            ReadData();
            throw new NotImplementedException();
        }

        public T Find(Predicate<T> predicate)
        {
            ReadData();
            return Query(predicate).FirstOrDefault();
        }

        public T Find(Predicate<T> predicate, int revision)
        {
            ReadData();
            return Query(predicate, revision).FirstOrDefault();
        }

        public Guid? Identify(T item)
        {
            MetaWrapper<T, MetaData> meta = item;
            if (item != null)
            {
                return meta.Meta.Id;
            }
            return null;
        }

        public void Prune(T item)
        {
            ReadData();
        }

        public void Prune(T item, int revision)
        {
            ReadData();
            throw new NotImplementedException();
        }

        public IQueryable<T> Query(Predicate<T> predicate)
        {
            ReadData();
            return Query(predicate, -1).Where(x => predicate.Invoke(x.Data)).Select(x => (T)x).AsQueryable<T>();
        }

        private IQueryable<MetaWrapper<T, MetaData>> Query(Predicate<T> predicate, int revision)
        {
            if (revision < 0)
            {
                return (from item in _data
                            group item by item.Meta.Id into g
                            select g.OrderByDescending(t => t.Meta.Revision).FirstOrDefault()).AsQueryable<MetaWrapper<T, MetaData>>();
            }
            else
            {
                return (from item in _data
                        group item by item.Meta.Id into g
                        select g.Where(t => t.Meta.Revision == revision).FirstOrDefault()).AsQueryable<MetaWrapper<T, MetaData>>();
            }
        }

        public T Store(T item)
        {
            ReadData();
            var wrapped = WrapItem(item);
            var latest = FindLatestRevision(wrapped);
            if (latest != null)
            {
                latest = latest.Clone();
                latest.Meta.Revision++;
                latest.Data = DeapClone<T>.Clone(wrapped.Data);
                latest.Meta.Modified = DateTime.Now;
            } else
            {
                latest = wrapped;
            }
            _data.Add(latest);
            WriteData();
            return latest.Clone();
        }

        protected abstract string BasePath { get; set; }
        protected abstract string DataFile { get; set; }
        protected abstract string IndexFile { get; set; }

        protected void ReadIndex()
        {
            var reader = new StringReader(_cache.ReadFile(IndexFile));
            _index = Settings.Serializer.Deserialize<Dictionary<Type, Guid>>(reader);
            if (_index == null) _index = new Dictionary<Type, Guid>();
        }

        protected void WriteIndex()
        {
            var writer = new StringWriter();
            Settings.Serializer.Serialize(writer, Settings.Format, _index);
            _cache.WriteFile(IndexFile, writer.ToString());
        }


        private void ReadData(bool force = false)
        {
            if (_data == null || force)
            {
                _data = _cache.Read<List<MetaWrapper<T, MetaData>>>(DataFile)
                    ?? new List<MetaWrapper<T, MetaData>>();
                _data = _data.Select(x => MetaWrapper<T, MetaData>.Register(x)).ToList();
            }
        }

        private void WriteData()
        {
            _cache.Write(DataFile, Settings.Format, _data);
        }

        private MetaWrapper<T, MetaData> FindLatestRevision(MetaWrapper<T, MetaData> item)
        {
            return _data
                .Where(x => x.Meta.Id == item.Meta.Id)
                .OrderByDescending(x => x.Meta.Revision)
                .FirstOrDefault();
        }

        protected Guid CheckIndex()
        {
            var type = typeof(T);
            if (_index == null) ReadIndex();
            if (_index.ContainsKey(type))
            {
                return _index[type];
            }
            _index.Add(type, Guid.NewGuid());
            WriteIndex();
            return _index[type];
        }

        private MetaWrapper<T, MetaData> WrapItem(T item)
        {
            MetaWrapper<T, MetaData> meta = item;
            if (meta == null)
            {
                meta = MetaWrapper<T, MetaData>.Create(item);
                meta.Meta = MetaData.New();
            }
            return meta;
        }



    }
}
