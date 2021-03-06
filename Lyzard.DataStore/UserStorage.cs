﻿/* 
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
using Lyzard.FileSystem;
using Lyzard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public class UserStorage<T> : Container<T>
        where T: class
    {

        public UserStorage(string container) : this(container, CacheManager.Instance)
        {
        }

        public UserStorage(string container, ICacheManager cacheManager)
            : base(container, cacheManager)
        {
            Settings.BaseLocation = CommonFolders.UserDataStore;
            var sep = CommonFolders.Sep;
            IndexFile = $"{BasePath}{sep}{container}.idx";
            DataFile = $"{BasePath}{sep}{CheckIndex().ToString()}.dat";
            CheckIndex();
        }

        protected override string DataFile { get; set; }
    }
}
