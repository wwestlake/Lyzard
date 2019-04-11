using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.TestConsole.TestClasses
{
    public class TestClass2 : MarshalByRefObject
    {
        public string Hello { get { return "Hello, Plugin World!"; } }
    }
}
