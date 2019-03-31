using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lyzard.Interfaces;
using Lyzard.Utilities;

namespace Lyzard.TestConsole
{
    public class CompilerTest : MarshalByRefObject, IPlugin
    {
        public string MyField;

        public void Hello()
        {
            Console.WriteLine("Hello, World!");
        }

        public void Test(string message)
        {
            Console.WriteLine(message);
        }

        public string Message(string message)
        {
            return message;
        }

        public string Property { get; set; }


        public string Name { get; set; }

        public void Initialize()
        {
            Console.WriteLine("Initializing Plugin");
        }

        public void StartPlugin()
        {
            Console.WriteLine("Plugin Starting Again....");
            Helpers.PrintLoadedAssemblies();
        }

    }
}
