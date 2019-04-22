using System.IO;
using Lyzard.Interfaces;

namespace Lyzard.Intercaces
{
    public interface IManagedFile
    {
        string Extension { get; }
        string FileName { get; }
        string FilePath { get; }
        string FullPath { get; }
        ISerializer Serializer { get; set; }

        event FileSystemEventHandler Changed;

        void Append(string text);
        void Delete();
        string Load();
        T Load<T>();
        void Save(string text);
        void Save<T>(T item);
    }
}