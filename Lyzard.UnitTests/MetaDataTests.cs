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
using Lyzard.Collections;
using Lyzard.DataStore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.UnitTests
{
    public class Entity
    {
        public string Name { get; set; }
    }

    public class EntityMetaData
    {
        public string Id { get; set; }
    }

    [TestFixture]
    public class MetaDataTests
    {
        [Test]
        public void MetaWrapper_wraps_item_and_cast_identity_both_ways()
        {
            var entity = MetaWrapper<Entity, EntityMetaData>.Create(new Entity() { Name = "Test" } );
            Assert.That(entity is Entity && ((MetaWrapper<Entity, EntityMetaData>)entity is MetaWrapper<Entity, EntityMetaData>));
        }

        [Test]
        public void MetaWrapper_casts_item()
        {
            var id = Guid.NewGuid().ToString(); 
            var entity = MetaWrapper<Entity, EntityMetaData>.Create(new Entity() { Name = "Test" }, new EntityMetaData() { Id = id });
            var meta = (MetaWrapper<Entity, EntityMetaData>)entity;

            Assert.AreEqual(id, meta.Meta.Id);
        
        }

        [Test]
        public void MetaWrapper_GetMetaData_returns_null_if_item_not_registered()
        {
            var entity = new Entity();
            var result = MetaWrapper<Entity, EntityMetaData>.GetMetaData(entity);
            Assert.IsNull(result);
        }

        public void Meta_wrapper_identity_property_cast_is_same()
        {
            var entity = new Entity();
            var wrapped = MetaWrapper<Entity, EntityMetaData>
                .Create(entity, new EntityMetaData() { Id = Guid.NewGuid().ToString() });
            MetaWrapper<Entity, EntityMetaData> unwrapped = wrapped;
            Assert.AreEqual(unwrapped.Meta.Id, MetaWrapper<Entity, EntityMetaData>.GetMetaData(wrapped).Id);
        }

    }
}
