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
    public abstract class StorageContract : IStorageContract
    {
        private object lockObject = new object();
        private ManagedFile _indexFile;
        private Dictionary<Type, Guid> _index;
        private Dictionary<Guid, List<object>> _cache = new Dictionary<Guid, List<object>>();
        private Dictionary<Guid, ManagedFile> _files = new Dictionary<Guid, ManagedFile>();

        public StorageContract()
        {
            Settings = new StorageSettings();
        }

        public IStorageSettings Settings { get; set; }


        /// <summary>
        /// Finds the fist item matching predicate
        /// </summary>
        /// <typeparam name="T">The type of item to find</typeparam>
        /// <param name="predicate">The predicate</param>
        /// <returns>The item</returns>
        public T Find<T>(Predicate<T> predicate)
             where T : class
        {
            lock (lockObject)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Retrives all items of type T that where predicate returns true
        /// </summary>
        /// <typeparam name="T">The type of item to retreive</typeparam>
        /// <param name="predicate">The predicate</param>
        /// <returns>An enumeration of T</returns>
        public IEnumerable<T> Query<T>(Predicate<T> predicate)
             where T : class
        {
            lock (lockObject)
            {
                return null;
            }
        }

        /// <summary>
        /// Removes zero or more items from the data store
        /// </summary>
        /// <typeparam name="T">The type of item to remove</typeparam>
        /// <param name="predicate">The predicate</param>
        /// <returns>The number of items remove (may be zero)</returns>
        public int Remove<T>(Predicate<T> predicate)
             where T : class
        {
            lock (lockObject)
            {
                return 0;
            }
        }

        /// <summary>
        /// Stores an item in the appropriate data store
        /// </summary>
        /// <typeparam name="T">The type of item to store</typeparam>
        /// <param name="item">The item to store</param>
        public void Store<T>(T item)
             where T : class
        {
            lock (lockObject)
            {
                if (_index.ContainsKey(typeof(T)))
                {
                    UpdateIndexAndStore(item);
                }
                else
                {
                    CreatNewFileAndStore(item);
                }
            }
        }

        private void UpdateIndexAndStore<T>(T item) where T : class
        {
            var guid = _index[typeof(T)];
            if (_cache.ContainsKey(guid))
            {
                UpdateCacheAndStore(item, guid);
            }
            else
            {
                LoadListAndStore(item, guid);
            }
        }

        private void CreatNewFileAndStore<T>(T item) where T : class
        {
            var guid = Guid.NewGuid();
            _index.Add(typeof(T), guid);
            SaveIndex();
            var list = new List<T>();
            AddItemToList(item, guid, list);
            _cache[guid] = list.Select(x => x as object).ToList();
        }

        private void LoadListAndStore<T>(T item, Guid guid) where T : class
        {
            var list = LoadList<T>(guid);
            AddItemToList(item, guid, list);
            _cache[guid] = list.Select(x => x as object).ToList();
        }

        private void UpdateCacheAndStore<T>(T item, Guid guid) where T : class
        {
            var list = _cache[guid];
            AddItemToList(item, guid, list);
            _cache[guid] = list.Select(x => x as object).ToList();
        }

        private List<T> LoadList<T>(Guid guid)
        {
            ManagedFile file;
            if (_files.ContainsKey(guid))
            {
                file = _files[guid];
            } else
            {
                var path = Settings.BaseLocation + CommonFolders.Sep + guid.ToString();
                file = ManagedFile.Create(path);
                _files.Add(guid, file);
            }
            return JsonConvert.DeserializeObject<List<T>>(file.Load());
        }

        private void AddItemToList<T>(T item, Guid guid, List<T> list) where T : class
        {
            var oldItem = list.FirstOrDefault(it => it.Equals(item));
            if (oldItem != null)
            {
                list.Remove(oldItem);
            }
            list.Add(item);
            Store(guid, list);
        }


        #region Private Methods
        private void Store<T>(Guid guid, List<T> list)
        {
            ManagedFile file;

            if (_files.ContainsKey(guid))
            {
                file = _files[guid];
            } else
            {
                var path = Settings.BaseLocation + CommonFolders.Sep + guid.ToString();
                file = ManagedFile.Create(path);
                _files.Add(guid, file);
            }
            file.Save(JsonConvert.SerializeObject(list));
        }

        private void SaveIndex()
        {
            _indexFile.Save(JsonConvert.SerializeObject(_index));
        }

        protected void LoadIndex()
        {
            _indexFile = ManagedFile.Create(Settings.IndexFile);
            var text = _indexFile.Load();
            if (string.IsNullOrEmpty(text))
            {
                _index = new Dictionary<Type, Guid>();
                text = JsonConvert.SerializeObject(_index);
            } else
            {
                _index = JsonConvert.DeserializeObject<Dictionary<Type, Guid>>(text);
            }
        }
       
        

        #endregion


    }
}
