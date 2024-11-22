using School.Data.Entities;
using School.Data.Helpers;

namespace School.Services.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentByIdAsyncWithInclude(int id);
        Task<Student> GetStudentByIdAsync(int id);
        Task<string> AddAsync(Student student);
        Task<bool> IsExist(string name);
        Task<bool> IsExistExcludeSelf(string name, int id);
        Task<string> EditAsync(Student student);
        Task<string> DeleteAsync(Student student);
        IQueryable<Student> GetQuearableStudents();
        IQueryable<Student> FilterQuerable(StudentOrderingEnum orderby, string search);
    }
}
