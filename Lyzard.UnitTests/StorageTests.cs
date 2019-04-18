// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using Lyzard.DataStore;
using NUnit.Framework;
using Moq;
using System;

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
        private IStorageContract<SimpleEntity> storage;
        private Mock<IStorageContract<MetaData<SimpleEntity>>> _manager;

        MetaData<SimpleEntity> _entity = null;


        [SetUp]
        public void Setup()
        {
            _manager = new Mock<IStorageContract<MetaData<SimpleEntity>>>();
            StorageFactory<SimpleEntity>.Instance.SetSystemStorageManager(_manager.Object);
            _manager.Setup(x => x.Store(It.IsAny<MetaData<SimpleEntity>>())).Callback<MetaData<SimpleEntity>>(x => {
                _entity = x;
            } ).Returns(() => _entity);
            storage = StorageFactory<SimpleEntity>.Instance.SystemStorage;

        }

        [Test]
        public void StorageFactory_returns_system_storage_instance()
        {
            var store = StorageFactory<SimpleEntity>.Instance.SystemStorage;
            Assert.That(store, Is.Not.Null, "Store is null?");
        }

        [Test]
        public void StorageFactory_returns_user_storage_instance()
        {
            var store = StorageFactory<SimpleEntity>.Instance.UserStorage;
            Assert.That(store, Is.Not.Null, "Store is null?");
        }

        [Test]
        public void Storage_unit_passes_entity_in_as_meta_data_object()
        {
            var entity = storage.Store(new SimpleEntity() { Name = "Test" });
            _manager.Verify();
            Assert.IsNotNull(entity);
        }

        public void Storage_unit_finds_meta_data_object_in_cache()
        {
            var entity = storage.Store(new SimpleEntity() { Name = "Test" });
            _manager.Verify();
            Assert.IsNotNull(entity);
        }

    }
}
