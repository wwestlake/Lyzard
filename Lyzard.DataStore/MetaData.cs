using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.DataStore
{
    public sealed class MetaData
    {

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int Revision { get; set; }

        public static MetaData New()
        {
            return new MetaData
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Revision = 0
            };
        }

    }
}
