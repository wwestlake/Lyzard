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
