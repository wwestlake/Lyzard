using Lyzard.Interfaces;
using Lyzard.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Collections
{
    public static class DeapClone<T>
    {
        public static T Clone(T item)
        {
            var serializer = new JsonSerializer();
            var writer = new StringWriter();
            serializer.Serialize(writer, Format.None, item);
            var reader = new StringReader(writer.ToString());
            return serializer.Deserialize<T>(reader);
        }

    }
}
