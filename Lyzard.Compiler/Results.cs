using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Compiler
{
    public class Results
    {
        public Results()
        {
            Errors = new List<Error>();
        }

        public Assembly Assembly { get; set; }
        public IList<Error> Errors { get; set; }

        public string PathToAssembly { get; set; }

        public bool HasErrors { get { return Errors.Count > 0; } }

    }
}
