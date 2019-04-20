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
