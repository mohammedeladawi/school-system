using Microsoft.AspNetCore.Http;

namespace SchoolProject.Application.Interfaces.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string folderPath);
}