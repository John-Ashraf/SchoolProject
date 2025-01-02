using School.Data.Entities.Identity;

namespace School.Services.Abstracts
{
    public interface IApplicationUserService
    {
        public Task<string> AddUser(AppUser user, string password);
    }
}
