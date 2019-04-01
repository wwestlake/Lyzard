using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Core
{
    public class TextOutput : BaseExecutionBlock
    {
        /// <summary>
        /// Constructs a ConsoleOutput execution block.
        /// </summary>
        public TextOutput()
        {
            AddInput<string>("Text", "Hello, World!");
        }

        public TextWriter Writer { get; set; } = Console.Out;

        /// <summary>
        /// Writes the supplied value to the console
        /// </summary>
        protected override void Operation()
        {
            var v = GetValue("Text");
            var value = v.ToString();
            if (value != null) Writer.WriteLine(value);
        }
    }
}
