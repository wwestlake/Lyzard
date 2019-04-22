using Lyzard.Interfaces;
using Lyzard.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.UnitTests
{
    public class MockCacheManager : ICacheManager
    {

        Dictionary<string, string> _dummyFiles = new Dictionary<string, string>();

        public ISerializer Serializer { get; set; }

        public MockCacheManager()
        {
            Serializer = new JsonSerializer();
        }

        public void Delete(string path)
        {
            if (_dummyFiles.ContainsKey(path))
            {
                _dummyFiles.Remove(path);
            }
        }

        public T Read<T>(string path)
        {
            var reader = new StringReader(ReadFile(path));
            return Serializer.Deserialize<T>(reader);
        }

        public string ReadFile(string path)
        {
            if (_dummyFiles.ContainsKey(path))
            {
                return _dummyFiles[path];
            }
            return string.Empty;
        }

        public void Write<T>(string path, Format format, T item)
        {
            var writer = new StringWriter();
            Serializer.Serialize(writer, format, item);
            WriteFile(path, writer.ToString());
        }

        public void WriteFile(string path, string text)
        {
            if (_dummyFiles.ContainsKey(path))
            {
                _dummyFiles[path] = text;
            } else
            {
                _dummyFiles.Add(path, text);
            }
        }


    }
}
