using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public sealed class MetaData
    {
        public Guid Id { get; internal set; }
        public DateTime Created { get; internal set; }
        public DateTime Modified { get; internal set; }
        public int Revision { get; internal set; }
    }
}
