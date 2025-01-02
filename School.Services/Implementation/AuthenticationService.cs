using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Data.Helpers;
using School.infrastructure.Abstracts;
using School.infrastructure.Data;
using School.Services.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace School.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _dbcontext;
        private readonly IRefreshTokenInf _userRefreshTokenInf;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructor
        public AuthenticationService(JwtSettings jwtSettings,
            UserManager<AppUser> userManager, IRefreshTokenInf refreshTokenInf, ApplicationDbContext context, IEmailService emailService)
        {
            _jwtSettings = jwtSettings;
            _dbcontext = context;
            _userManager = userManager;
            _userRefreshTokenInf = refreshTokenInf;
            _emailService = emailService;
        }
        #endregion

        #region HandleFunctions
        public async Task<JWTAuthRes> GetJWTToken(AppUser user)
        {
            var (jwtToken, accessToken) = await GenerateJWT(user);
            var refreshToken = GetRefreshToken(username: user.UserName);
            var UserRefreshToken = new UserRefershToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = refreshToken.Expireat,
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.Token,
                Token = accessToken,
                AppUserId = user.Id,
            };
            _ = await _userRefreshTokenInf.AddAsync(UserRefreshToken);


            var JWTAuthRes = new JWTAuthRes();
            JWTAuthRes.AccessToken = accessToken;
            JWTAuthRes.RefreshToken = refreshToken;
            return JWTAuthRes;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWT(AppUser user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
              _jwtSettings.Issuer,
              _jwtSettings.Audience,
              claims: claims,
              expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
              SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);

        }
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                Expireat = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                Token = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<List<Claim>> GetClaims(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }

        public JwtSecurityToken ReadJWTToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var result = handler.ReadJwtToken(token);
            return result;
        }
        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken JwtToken, string accessToken, string RefreshToken)
        {
            if (JwtToken == null || !JwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (JwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }
            var userId = JwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var refreshtoken = _userRefreshTokenInf.GetTableNoTracking()
                .Where(x => x.Token == accessToken && x.RefreshToken == RefreshToken).FirstOrDefault();
            if (refreshtoken == null)
            {

                return ("RefreshToken is Invalid", null);
            }
            if (refreshtoken.ExpiryDate < DateTime.UtcNow)
            {
                refreshtoken.IsRevoked = true;
                refreshtoken.IsUsed = false;
                await _userRefreshTokenInf.UpdateAsync(refreshtoken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = refreshtoken.ExpiryDate;
            return (userId, expirydate);
        }
        public async Task<JWTAuthRes> GetRefreshToken(AppUser user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWT(user);
            var response = new JWTAuthRes();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.Token = refreshToken;
            refreshTokenResult.Expireat = (DateTime)expiryDate;
            response.RefreshToken = refreshTokenResult;
            return response;

        }

        public async Task<string> ConfirmEmail(string? userid, string? code)
        {
            if (userid == null || code == null)
                return "ErrorWhenConfirmEmail";
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null) { return "Useridisnotvalid"; }
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded)
                return "ErrorWhenConfirmEmail";
            return "Success";
        }

        public async Task<string> SendResetPasswordCode(string Email)
        {
            var user = await _userManager.FindByIdAsync(Email);
            if (user == null) { return "UserNotFound"; }
            var trans = _dbcontext.Database.BeginTransaction();
            try
            {
                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                user.Code = randomNumber;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return "ErrorInUpdateUser";

                var message = "Code To Reset Passsword : " + user.Code;
                //Send Code To  Email 
                _ = await _emailService.SendEmail(user.Email, message, "Reset Password");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }



        }
        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            //Get User
            //user
            var user = await _userManager.FindByEmailAsync(Email);
            //user not Exist => not found
            if (user == null)
                return "UserNotFound";
            //Decrept Code From Database User Code
            var userCode = user.Code;
            //Equal With Code
            if (userCode == Code) return "Success";
            return "Failed";
        }
        public async Task<string> ResetPassword(string Email, string password)
        {

            var trans = await _dbcontext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                _ = await _userManager.RemovePasswordAsync(user);
                if (!await _userManager.HasPasswordAsync(user))
                {
                    _ = await _userManager.AddPasswordAsync(user, password);
                }
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }


    }


    #endregion

}

