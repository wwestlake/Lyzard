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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.TestConsole.TestClasses
{
    public class TestClass1 : MarshalByRefObject
    {
        private static TestClass1 _instance;
        private TestClass2 _testClass2;

        public static TestClass1 Instance
        {
            get { return _instance ?? (_instance = new TestClass1()); }
        }

        public TestClass2 TestClass2
        {
            get { return _testClass2 ?? (_testClass2 = new TestClass2()); }
        }

    }
}
