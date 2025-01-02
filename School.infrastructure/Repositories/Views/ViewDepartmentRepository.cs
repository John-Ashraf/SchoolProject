using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Views;
using School.infrastructure.Data;
using School.infrastructure.InfrastructureBases;
using School.infrastructure.Views;

namespace School.infrastructure.Repositories.Views
{
    public class ViewDepartmentRepository : GenericRepoAsync<DepartmentView>, IViewRepository<DepartmentView>
    {
        #region Fields
        private DbSet<DepartmentView> viewDepartment;
        #endregion

        #region Constructors
        public ViewDepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            viewDepartment = dbContext.Set<DepartmentView>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}
