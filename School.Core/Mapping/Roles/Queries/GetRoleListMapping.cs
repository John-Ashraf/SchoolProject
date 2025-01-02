using Microsoft.AspNetCore.Identity;
using School.Core.Features.Authorization.Queries.Response;

namespace School.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRoleListMappingMethod()
        {
            _ = CreateMap<IdentityRole, GetRolesListResponse>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
