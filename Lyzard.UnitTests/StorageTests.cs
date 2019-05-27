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

// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using Lyzard.Collections;
using Lyzard.DataStore;
using Lyzard.Interfaces;
using Lyzard.Serialization;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace Lyzard.UnitTests
{
    public class SimpleEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [TestFixture]
    public class StorageTests
    {
        ICacheManager _cache = new MockCacheManager();


        [SetUp]
        public void Setup()
        {
            StorageFactory<SimpleEntity>.SystemStorage("Test",_cache).Settings.Serializer = new JsonSerializer();
            StorageFactory<SimpleEntity>.UserStorage("Test",_cache).Settings.Serializer = new JsonSerializer();
        }

        [Test]
        public void StorageFactory_returns_system_storage_instance()
        {
            var store = StorageFactory<SimpleEntity>.SystemStorage("test", _cache);
            Assert.That(store, Is.Not.Null, "Store is null?");
        }

        [Test]
        public void StorageFactory_returns_user_storage_instance()
        {
            var store = StorageFactory<SimpleEntity>.UserStorage("test", _cache);
            Assert.That(store, Is.Not.Null, "Store is null?");
        }

        [Test]
        public void Storage_system_serialzes_an_object()
        {
            var entity = new SimpleEntity() { Name = "Bill" };
            var storage = StorageFactory<SimpleEntity>.UserStorage("Test", _cache);
            MetaWrapper<SimpleEntity, MetaData> storedEntity = storage.Store(entity);
            SimpleEntity updateEntity = storedEntity;
            updateEntity.Name = "Updated Bill";
            storedEntity = storage.Store(updateEntity);
            Assert.That(storedEntity is MetaWrapper<SimpleEntity, MetaData>);
            storage.Purge();
        }

        [Test]
        public void Storage_system_queries_data()
        {
            var entity = new SimpleEntity() { Name = "Bill" };
            var storage = StorageFactory<SimpleEntity>.UserStorage("Test", _cache);
            var newEnt = storage.Store(entity);
            newEnt.Name = "Updated Bill";
            newEnt = storage.Store(entity);
            var list = storage.Query(x => true);
            Assert.That(list.Count() == 1);
            MetaWrapper<SimpleEntity, MetaData> test = list.FirstOrDefault();
            Assert.That(test.Meta.Revision == 1);
            storage.Purge();
        }

    }
}
