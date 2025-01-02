using School.Data.Entities.Identity;

namespace School.Services.AuthServices.Interfaces
{
    public interface ICurrentUser
    {
        public Task<AppUser> GetUserAsync();
        public int GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
