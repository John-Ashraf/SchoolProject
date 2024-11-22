using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.infrastructure.Abstracts;
using School.infrastructure.Data;
using School.infrastructure.InfrastructureBases;

namespace School.infrastructure.Repositories
{
    public class InstructorRepo : GenericRepoAsync<Instructor>, IInstructorInf
    {
        #region Fields
        private DbSet<Instructor> _instructors;
        #endregion
        #region Constructor
        public InstructorRepo(ApplicationDbContext context) : base(context)
        {
            _instructors = context.Set<Instructor>();
        }
        #endregion
        #region HandleFunctions
        #endregion
    }
}
