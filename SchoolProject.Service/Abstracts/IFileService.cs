using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Abstracts
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderPath);
    }
}