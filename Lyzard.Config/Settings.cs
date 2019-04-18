using Lyzard.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lyzard.Config
{
    public class Settings
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public LogEntryType LogLevel { get; set; } = LogEntryType.Info;

    }
}
