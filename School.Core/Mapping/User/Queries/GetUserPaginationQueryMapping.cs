using School.Core.Features.User.Queries.Response;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void GetUserPaginationMapping()
        {
            _ = CreateMap<AppUser, GetUserListResponse>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(x => x.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}
//public string FullName { get; set; }
//public string Email { get; set; }
//public string? Address { get; set; }
//public string? Country { get; set; }