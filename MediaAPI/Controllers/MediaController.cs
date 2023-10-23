using MediaAPI_Service.Interfaces;
using MediaAPI_Utilities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;

namespace MediaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IFileStorage fileStorage;
        private readonly AppSettings _appSettings;

        public MediaController(IFileStorage fileStorage, IOptions<AppSettings> appSettings)
        {
            this.fileStorage = fileStorage;
            this._appSettings = appSettings.Value;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("Invalid file");
            }

            foreach (var file in files)
            {
                if (file.Length > 500 * 1024 * 1024) // 500MB
                {
                    return BadRequest($"{file.Name} file size exceeds the limit");
                }

                if (fileStorage.FileExists(file.FileName))
                {
                    return Ok($"{file.Name} file already exists");
                }

                using var stream = file.OpenReadStream();
                await fileStorage.StoreFileAsync(stream, file.FileName);
            }

            return Ok("File uploaded successfully");
        }

        [HttpGet("list")]
        public IActionResult ListFiles()
        {
            var files = Directory.GetFiles(_appSettings.Path);
            var fileInfos = files.Select(file => new
            {
                Name = Path.GetFileName(file),
                Size = new FileInfo(file).Length,
                Date = System.IO.File.GetLastWriteTime(file)
            });

            return Ok(fileInfos);
        }
    }
}