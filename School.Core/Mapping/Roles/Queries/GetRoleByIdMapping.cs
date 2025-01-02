using Microsoft.AspNetCore.Identity;
using School.Core.Features.Authorization.Queries.Response;

namespace School.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRoleByIdMappingMethod()
        {
            _ = CreateMap<IdentityRole, GetRoleByIdResponse>();
        }
    }
}
