using School.Data.Entities.Identity;
using School.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace School.Services.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JWTAuthRes> GetJWTToken(AppUser user);
        public Task<JWTAuthRes> GetRefreshToken(AppUser user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken JwtToken, string accessToken, string RefreshToken);
        public JwtSecurityToken ReadJWTToken(string token);
        public Task<string> ValidateToken(string accessToken);
        public Task<string> ConfirmEmail(string userid, string code);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ConfirmResetPassword(string Code, string Email);
        public Task<string> ResetPassword(string Email, string password);
    }
}
