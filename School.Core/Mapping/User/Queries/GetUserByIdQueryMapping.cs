using School.Core.Features.User.Queries.Response;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            _ = CreateMap<AppUser, GetUserByIdResponse>();
        }
    }
}
