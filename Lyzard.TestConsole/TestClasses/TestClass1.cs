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
