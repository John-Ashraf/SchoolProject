using School.Core.Features.User.Commands.Models;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void UpdateUserMapping()
        {
            _ = CreateMap<EditUserCommand, AppUser>();
        }
    }
}
