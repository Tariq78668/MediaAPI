using MediaAPI_Service.Interfaces;
using MediaAPI_Utilities.Models;
using Microsoft.Extensions.Options;

namespace MediaAPI_Service.Implementations
{
    public class FileStorage : IFileStorage
    {
        private readonly AppSettings _appSettings;

        public FileStorage(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<string> StoreFileAsync(Stream stream, string fileName)
        {
            string filePath = Path.Combine(_appSettings.Path, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }

            return filePath;
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(Path.Combine(_appSettings.Path, fileName));
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(_appSettings.Path, fileName);
        }
    }
}
