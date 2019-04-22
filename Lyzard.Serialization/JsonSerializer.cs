using Lyzard.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace Lyzard.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(TextReader reader)
        {
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }

        public void Serialize<T>(TextWriter writer, Format format, T item)
        {
            var text = JsonConvert.SerializeObject(item, FormatConverter.Convert(format));
            writer.Write(text);
        }
    }
}
