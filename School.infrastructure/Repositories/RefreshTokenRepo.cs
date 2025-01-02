using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Identity;
using School.infrastructure.Abstracts;
using School.infrastructure.Data;
using School.infrastructure.InfrastructureBases;

namespace School.infrastructure.Repositories
{
    public class RefreshTokenRepo : GenericRepoAsync<UserRefershToken>, IRefreshTokenInf
    {
        #region Fields
        private readonly DbSet<UserRefershToken> _userRefreshToken;
        #endregion
        #region Constructor
        public RefreshTokenRepo(ApplicationDbContext context) : base(context)
        {
            _userRefreshToken = context.Set<UserRefershToken>();

        }

        #endregion
        #region HandleFunctions

        #endregion
    }
}
