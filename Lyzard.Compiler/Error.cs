using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Compiler
{
    public class Error
    {
        public int Column { get; set; }
        public string ErrorNumber { get; set; }
        public string ErrorText { get; set; }
        public string FileName { get; set; }
        public bool IsWarning { get; set; }
        public int Line { get; set; }
    }
}
