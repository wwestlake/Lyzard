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
using Newtonsoft.Json;
using System.IO;

namespace Lyzard.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(TextReader reader)
        {
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }

        public void Serialize<T>(TextWriter writer, Format format, T item)
        {
            var text = JsonConvert.SerializeObject(item, FormatConverter.Convert(format) 
                ,new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            writer.Write(text);
        }
    }
}
