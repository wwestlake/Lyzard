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
