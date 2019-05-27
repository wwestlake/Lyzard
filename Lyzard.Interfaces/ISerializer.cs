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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Interfaces
{

    public enum Format {
        None,
        Indented
    }

    /// <summary>
    /// Represents the contract for serializing and deserializing information.
    /// This contract is meant for "non-binary" serialization
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Writes the serialized data to text writer
        /// </summary>
        /// <param name="writer">The text writer to write to</param>
        /// <param name="item">The item to serialize</param>
        void Serialize<T>(TextWriter writer, Format format, T item);

        /// <summary>
        /// Reads from the text reader and returns the item T
        /// </summary>
        /// <param name="reader">The reader to read from</param>
        /// <returns>The item deserialized</returns>
        T Deserialize<T>(TextReader reader);
    }
}
