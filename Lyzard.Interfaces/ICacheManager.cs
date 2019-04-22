using Lyzard.Interfaces;

namespace Lyzard.Interfaces
{
    public interface ICacheManager
    {
        ISerializer Serializer { get; set; }

        void Delete(string path);
        T Read<T>(string path);
        string ReadFile(string path);
        void Write<T>(string path, Format format, T item);
        void WriteFile(string path, string text);
    }
}