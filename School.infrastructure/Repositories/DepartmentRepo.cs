using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.infrastructure.Abstracts;
using School.infrastructure.Data;
using School.infrastructure.InfrastructureBases;

namespace School.infrastructure.Repositories
{
    public class DepartmentRepo : GenericRepoAsync<Department>, IDepartmentInf
    {
        #region Fields
        private readonly DbSet<Department> _department;
        #endregion
        #region Constructor
        public DepartmentRepo(ApplicationDbContext context) : base(context)
        {
            _department = context.Set<Department>();

        }

        #endregion
        #region HandleFunctions
        #endregion
    }
}
