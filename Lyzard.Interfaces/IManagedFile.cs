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
using System.IO;
using Lyzard.Interfaces;

namespace Lyzard.Interfaces
{
    public interface IManagedFile
    {
        string Extension { get; }
        string FileName { get; }
        string FilePath { get; }
        string FullPath { get; }
        ISerializer Serializer { get; set; }

        event FileSystemEventHandler Changed;

        void Append(string text);
        void Delete();
        string Load();
        T Load<T>();
        void Save(string text);
        void Save<T>(T item);
    }
}