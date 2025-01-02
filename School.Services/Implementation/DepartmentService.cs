using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.infrastructure.Abstracts;
using School.Services.Abstracts;

namespace School.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentInf _departmentRepo;
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentInf departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        #endregion

        #region HandleFunctions
        public async Task<Department> GetDepartmentById(int id)
        {
            return await _departmentRepo.GetTableNoTracking().Where(dept => dept.DID.Equals(id))
                  .Include(dept => dept.Instructors)
                  .Include(dept => dept.DepartmentSubjects).ThenInclude(DepartmentSubjects => DepartmentSubjects.Subject)
                  .Include(dept => dept.Instructor)
                  .FirstOrDefaultAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _departmentRepo.GetTableNoTracking().AnyAsync(x => x.DID == id);
        }

        #endregion
    }
}
