using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class StorageFactory<T>
        where T: class
    {
        private static StorageFactory<T> _instance;
        private SystemStorage<T> _systemStorage;
        private UserStorage<T> _userStorage;
        private IStorageContract<MetaData<T>> _systemStorageManager;
        private IStorageContract<MetaData<T>> _userStorageManager;


        public void SetSystemStorageManager(IStorageContract<MetaData<T>> manager)
        {
            _systemStorageManager = manager;
        }

        public void SetUserStorageManager(UserStorageManager<T> manager)
        {
            _userStorageManager = manager;
        }

        public static StorageFactory<T> Instance
        {
            get
            {
                return _instance ?? (_instance = new StorageFactory<T>());
            }
        }

        public StorageContract<T> SystemStorage
        {
            get
            {
                if (_systemStorageManager == null)
                    return _systemStorage ?? (_systemStorage = new SystemStorage<T>());
                else
                    return _systemStorage ?? (_systemStorage = new SystemStorage<T>(_systemStorageManager));
            }
        }

        public StorageContract<T> UserStorage
        {
            get
            {
                if (_userStorageManager == null)
                    return _userStorage ?? (_userStorage = new UserStorage<T>());
                else
                    return _userStorage ?? (_userStorage = new UserStorage<T>(_userStorageManager));
            }
        }

    }
}
