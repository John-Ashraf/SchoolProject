using Microsoft.AspNetCore.Http;

namespace School.Services.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImage(string Location, IFormFile file);
    }
}
