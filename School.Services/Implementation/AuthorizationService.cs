using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Core.Features.Authorization.Queries.Response;
using School.Data.DTOS;
using School.Data.Entities.Identity;
using School.Data.Helpers;
using School.Data.Requests;
using School.infrastructure.Data;
using School.Services.Abstracts;
using System.Security.Claims;

namespace School.Services.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        #endregion
        #region Constructor
        public AuthorizationService(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager, ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        #endregion
        #region Handle Functions

        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var Role = await _roleManager.FindByIdAsync(request.Id);
            if (Role == null)
            {
                return "NotFound";
            }
            Role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(Role);
            if (result.Succeeded)
                return "Success";
            else
            {

                var errors = string.Join("-", result.Errors);
                return errors;
            }
        }

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string> DeleteRoleAsync(int RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId.ToString());

            if (role == null) return "NotFound";
            //Chech if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return exception 
            if (users != null && users.Count() > 0) return "Used";
            //delete
            var result = await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded) return "Success";
            //problem
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<List<IdentityRole>> GetRolesList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<IdentityRole> GetRoleById(string id)
        {
            var role = await _roleManager.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();
            return role;
        }

        public async Task<ManageUserRolesResponse> ManageUserRolesData(AppUser user)
        {
            var response = new ManageUserRolesResponse();
            var roles = _roleManager.Roles.ToList();
            _ = _userManager.GetRolesAsync(user);
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var tmpRole = new Role();
                tmpRole.Id = role.Id;
                tmpRole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    tmpRole.HasRole = true;
                }
                else
                {
                    tmpRole.HasRole = false;
                }
                response.Roles.Add(tmpRole);
            }
            return response;
        }

        public async Task<string> UpdateUserRole(UpdateUserRolesRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return "UserNotFound";
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var UserRolesPrevious = await _userManager.GetRolesAsync(user);
                var deletepreviousresult = await _userManager.RemoveFromRolesAsync(user, UserRolesPrevious);
                if (!deletepreviousresult.Succeeded) return "Failed To RemoveOldRoles";

                var selectedRoles = request.Roles.Where(x => x.HasRole == true).ToList();
                foreach (var role in selectedRoles)
                {
                    var tmpres = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!tmpres.Succeeded) { return "Failed To AddNewRoles"; }

                }
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "Failed To UpdateUserRoles";
            }
        }

        public async Task<ManageUserClaimsResponse> ManageUserClaimData(AppUser User)
        {
            var response = new ManageUserClaimsResponse();
            var UserClaimList = await _userManager.GetClaimsAsync(User);
            response.UserId = User.Id;
            foreach (var claim in ClaimsStore.claims)
            {
                Data.DTOS.UserClaims tmpClaim = new Data.DTOS.UserClaims();
                tmpClaim.Type = claim.Type;

                if (UserClaimList.Any(x => x.Type == claim.Type))
                {
                    tmpClaim.Value = true;
                }
                else
                {
                    tmpClaim.Value = false;
                }
                response.userClaims.Add(tmpClaim);

            }
            return response;
        }

        public async Task<string> UpdateUserClaims(ManageUserClaimsRequest request)
        {
            var trans = _dbContext.Database.BeginTransaction();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return "UserIsNotFound";
                }
                var UserClaims = await _userManager.GetClaimsAsync(user);
                var removeresult = await _userManager.RemoveClaimsAsync(user, UserClaims);
                if (!removeresult.Succeeded)
                {
                    return "FailedToRemoveOldClaims";
                }
                var newclaims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var addresult = await _userManager.AddClaimsAsync(user, newclaims);
                if (!addresult.Succeeded)
                {
                    return "FailedToAddNewClaims";
                }
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {

                trans.Rollback();
                return "FailedToUpdateClaims";
            }

        }


        #endregion
    }
}
