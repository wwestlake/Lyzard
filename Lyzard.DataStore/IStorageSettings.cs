namespace Lyzard.DataStore
{
    public interface IStorageSettings
    {
        string BaseLocation { get; set; }
        string IndexFile { get; set; }
        string Container { get; set; }
    }
}