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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public static class StorageFactory<T>
        where T: class
    {

        private static Dictionary<string, UserStorage<T>> _userInstances = new Dictionary<string, UserStorage<T>>();
        private static Dictionary<string, SystemStorage<T>> _systemInstances = new Dictionary<string, SystemStorage<T>>();

        public static IStorageContract<T> UserStorage(string container)
        {
            if (! _userInstances.ContainsKey(container)) {
                _userInstances.Add(container, new UserStorage<T>(container));
            }
            return _userInstances[container];
        }

        public static IStorageContract<T> UserStorage(string container, ICacheManager cacheManager)
        {
            if (!_userInstances.ContainsKey(container))
            {
                _userInstances.Add(container, new UserStorage<T>(container, cacheManager));
            }
            return _userInstances[container];
        }


        public static IStorageContract<T> SystemStorage(string container)
        {
            if (!_systemInstances.ContainsKey(container))
            {
                _systemInstances.Add(container, new SystemStorage<T>(container));
            }
            return _systemInstances[container];
        }

        public static IStorageContract<T> SystemStorage(string container, ICacheManager cacheManager)
        {
            if (!_systemInstances.ContainsKey(container))
            {
                _systemInstances.Add(container, new SystemStorage<T>(container, cacheManager));
            }
            return _systemInstances[container];
        }

    }
}
