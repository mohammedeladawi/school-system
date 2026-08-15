using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Interfaces.Services;

namespace SchoolProject.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folderPath)
    {
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var relativePath = fullPath.Replace(_webHostEnvironment.WebRootPath, "").Replace("\\", "/");
        return relativePath;
    }
}
