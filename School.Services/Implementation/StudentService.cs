using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Helpers;
using School.infrastructure.Abstracts;
using School.Services.Abstracts;

namespace School.Services.Implementation
{
    public class StudentService : IStudentService
    {
        #region fields
        private readonly IStudentInf _studentInf;

        #endregion
        #region constructors
        public StudentService(IStudentInf studentInf)
        {
            _studentInf = studentInf;
        }

        public async Task<string> AddAsync(Student student)
        {
            var studentResult = _studentInf.GetTableNoTracking().Where(st => st.Name == student.Name).FirstOrDefault();
            if (studentResult != null) return "Exist";

            await _studentInf.AddAsync(student);
            return "Success";
        }

        #endregion
        #region handle fucntions
        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentInf.GetStudentsAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = _studentInf.GetTableNoTracking()
                  .Where(x => x.StudID == id).FirstOrDefault();
            return student;
        }
        public async Task<Student> GetStudentByIdAsyncWithInclude(int id)
        {
            var student = _studentInf.GetTableNoTracking().Include(st => st.Department)
                  .Where(x => x.StudID == id).FirstOrDefault();
            return student;
        }

        public async Task<bool> IsExist(string name)
        {
            return await _studentInf.GetTableNoTracking().AnyAsync(st => st.Name == name);
        }

        public async Task<bool> IsExistExcludeSelf(string name, int id)
        {
            return await _studentInf.GetTableNoTracking().Where(st => st.StudID != id && st.Name == (name)).FirstOrDefaultAsync() != null;
        }
        public async Task<string> EditAsync(Student student)
        {
            await _studentInf.UpdateAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentInf.BeginTransaction();
            try
            {
                await _studentInf.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public IQueryable<Student> GetQuearableStudents()
        {
            return _studentInf.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> FilterQuerable(StudentOrderingEnum orderby, string search)
        {
            var query = _studentInf.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search) || x.Address.Contains(search));
            }
            switch (orderby)
            {
                case StudentOrderingEnum.StudID:
                    query = query.OrderBy(x => x.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    query = query.OrderBy(x => x.Name);
                    break;
                case StudentOrderingEnum.Departmentname:
                    query = query.OrderBy(x => x.Department.DName);
                    break;
                case StudentOrderingEnum.Address:
                    query = query.OrderBy(x => x.Address);
                    break;

            }

            return query;

        }
        #endregion


    }
}
