namespace Lyzard.DataStore
{
    public enum Format { None, Indented}

    public interface IStorageSettings
    {
        string BaseLocation { get; set; }
        string IndexFile { get; set; }
        string Container { get; set; }
        Format Format { get; set; }
    }
}