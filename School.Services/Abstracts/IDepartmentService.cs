using School.Data.Entities;

namespace School.Services.Abstracts
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentById(int id);
        public Task<bool> IsExist(int id);
    }
}
