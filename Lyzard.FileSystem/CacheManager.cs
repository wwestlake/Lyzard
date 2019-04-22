using Lyzard.FileSystem;
using Lyzard.Interfaces;
using Lyzard.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lyzard.DataStore
{
    public sealed class CacheManager : ICacheManager
    {
        private static CacheManager _instance;
        private Dictionary<string, Tuple<ManagedFile, string>> _cache = new Dictionary<string, Tuple<ManagedFile, string>>();


        private CacheManager()
        {
        }

        public ISerializer Serializer { get; set; } = new JsonSerializer();

        public static CacheManager Instance
        {
            get { return _instance ?? (_instance = new CacheManager()); }
        }

        public string ReadFile(string path) => ReadCache(path);
        public void WriteFile(string path, string text) => WriteCache(path, text);

        public T Read<T>(string path) => Serializer.Deserialize<T>(new StringReader(ReadCache(path)));

        public void Write<T>(string path, Format format, T item)
        {
            var writer = new StringWriter();
            Serializer.Serialize(writer, format, item);
            WriteCache(path, writer.ToString());
        }

        public void Delete(string path)
        {
            if (_cache.ContainsKey(path))
            {
                _cache[path].Item1.Delete();
                _cache.Remove(path);
            }
        }

        ///////////////////////
        /// private methods
        /// ///////////////////

        private string ReadCache(string path)
        {
            if (_cache.ContainsKey(path))
            {
                return _cache[path].Item2;
            }
            else
            {
                var file = ManagedFile.Create(path);
                var contents = file.Load();
                file.Changed += File_Changed;
                _cache[path] = Tuple.Create(file, contents);
                return contents;
            }
        }

        private void WriteCache(string path, string text)
        {
            if (_cache.ContainsKey(path))
            {
                var file = _cache[path].Item1;
                _cache[path] = Tuple.Create(file, text);
                file.Save(text);
            }
            else
            {
                var file = ManagedFile.Create(path);
                _cache.Add(path, Tuple.Create(file, text));
                file.Save(text);
            }
        }

        private void File_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            var file = _cache[e.FullPath].Item1;
            _cache[e.FullPath] = Tuple.Create(file, file.Load());
        }
    }
}
