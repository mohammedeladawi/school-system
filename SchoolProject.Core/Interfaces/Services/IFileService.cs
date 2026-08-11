using Microsoft.AspNetCore.Http;

namespace SchoolProject.Core.Interfaces.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string folderPath);
}