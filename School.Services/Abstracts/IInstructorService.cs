using Microsoft.AspNetCore.Http;
using School.Data.Entities;

namespace School.Services.Abstracts
{
    public interface IInstructorService
    {
        // public Task<decimal> GetSalarySummationOfInstructor();
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);
        public Task<string> AddInstructorAsync(Instructor instructor, IFormFile file);

    }
}
