using Microsoft.AspNetCore.Identity;
using School.Core.Features.Authorization.Queries.Response;
using School.Data.DTOS;
using School.Data.Entities.Identity;
using School.Data.Requests;

namespace School.Services.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);

        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> DeleteRoleAsync(int RoleId);
        public Task<List<IdentityRole>> GetRolesList();
        public Task<IdentityRole> GetRoleById(string id);
        public Task<ManageUserRolesResponse> ManageUserRolesData(AppUser user);
        public Task<string> UpdateUserRole(UpdateUserRolesRequest request);
        public Task<string> UpdateUserClaims(ManageUserClaimsRequest request);
        public Task<ManageUserClaimsResponse> ManageUserClaimData(AppUser User);
    }
}
