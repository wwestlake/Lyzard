using Lyzard.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Serialization
{
    public static class FormatConverter
    {
        public static Formatting Convert(Format format)
        {
            switch (format)
            {
                case Format.Indented: return Formatting.Indented;
                case Format.None: return Formatting.None;
                default: return Formatting.None;
            }
        }
    }
}
