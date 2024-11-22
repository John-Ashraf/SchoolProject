using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.infrastructure.Abstracts;
using School.infrastructure.Data;
using School.infrastructure.InfrastructureBases;

namespace School.infrastructure.Repositories
{
    public class SubjectRepo : GenericRepoAsync<Subject>, ISubjectInf
    {
        #region Fields
        private DbSet<Subject> _subjects;
        #endregion
        #region Constructor
        public SubjectRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _subjects = dbContext.Set<Subject>();
        }
        #endregion
        #region HandleFunctions
        #endregion

    }
}
