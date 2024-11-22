using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.infrastructure.Abstracts;
using School.infrastructure.Data;
using School.infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.infrastructure.Repositories
{
    public class StudentRepo :GenericRepoAsync<Student>, IStudentInf
    {
        #region definiations
        private readonly DbSet<Student> _students;
        #endregion
        #region Constructor
        public StudentRepo(ApplicationDbContext context):base(context)
        {
            _students = context.Set<Student>();   
        }
        #endregion
        #region HandleFunctions
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _students.Include(st=>st.Department).ToListAsync();
        }
        #endregion
    }
}
