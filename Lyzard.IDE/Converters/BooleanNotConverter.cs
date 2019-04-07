using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.Converters
{
    public sealed class BooleanNotConverter : BooleanConverter<bool>
    {
        public BooleanNotConverter() :
          base(false, true)
        { }
    }
}
