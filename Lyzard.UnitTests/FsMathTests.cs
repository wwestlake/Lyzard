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
using Lyzard.FsMath;
using Microsoft.FSharp.Core;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.UnitTests
{
    [TestFixture]
    public class FsMathTests
    {
        [Test]
        public void RungeKutta_solver_test_simple_ODE()
        {
            var func = Converters.convert3<double,double,double>( (x, y) => Math.Exp(x) );
            var result = Solvers.RungeKutta4(0.0, 1.0, 0.01, 1.0, func);
            Assert.That(result.Length == 100);
        }
    }
}
