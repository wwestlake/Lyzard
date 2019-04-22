using Lyzard.Interfaces;

namespace Lyzard.Interfaces
{

    public interface IStorageSettings
    {
        string BaseLocation { get; set; }
        Format Format { get; set; }
        ISerializer Serializer { get; set; }
    }
}