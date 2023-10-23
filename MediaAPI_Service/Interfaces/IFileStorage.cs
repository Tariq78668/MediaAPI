namespace MediaAPI_Service.Interfaces
{
    public interface IFileStorage
    {
        Task<string> StoreFileAsync(Stream fileStream, string fileName);
        bool FileExists(string fileName);
        string GetFilePath(string fileName);
    }
}
