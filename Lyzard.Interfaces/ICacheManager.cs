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

namespace Lyzard.Interfaces
{
    /// <summary>
    /// Interface definition for a CacheManager
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Returns the serializer for this cache
        /// </summary>
        ISerializer Serializer { get; set; }

        /// <summary>
        /// Deletes the cache file
        /// </summary>
        /// <param name="path">Path to cache file</param>
        void Delete(string path);

        /// <summary>
        /// Reads the cache file
        /// </summary>
        /// <typeparam name="T">The cache type</typeparam>
        /// <param name="path">The path</param>
        /// <returns>The contents of the cache file as T</returns>
        T Read<T>(string path);

        /// <summary>
        /// Retuns the contents of the cache file at path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The contents as a string</returns>
        string ReadFile(string path);

        /// <summary>
        /// Writes a formated string to the cache file
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="path">The path</param>
        /// <param name="format">The format</param>
        /// <param name="item">The item to write</param>
        void Write<T>(string path, Format format, T item);

        /// <summary>
        /// Writes a file to the path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="text">The string to write</param>
        void WriteFile(string path, string text);
    }
}