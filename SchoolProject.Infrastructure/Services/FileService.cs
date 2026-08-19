using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Interfaces.Services;

namespace SchoolProject.Infrastructure.Services;

public class FileService : IFileService
{
    public bool DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string webRootPath, string relativeFolderPath)
    {
        string folderPath = Path.Combine(webRootPath, relativeFolderPath);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"{relativeFolderPath}/{fileName}";
    }
}
