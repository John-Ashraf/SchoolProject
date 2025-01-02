using MediatR;
using Microsoft.AspNetCore.Identity;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Models;
using School.Data.DTOS;
using School.Data.Entities.Identity;
using School.Services.Abstracts;

namespace School.Core.Features.Authorization.Queries.Handler
{
    public class ClaimsQueryHandler : ResponseHandler, IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        #endregion

        #region Constructor
        public ClaimsQueryHandler(UserManager<AppUser> userManager, IAuthorizationService AuthorizationService)
        {
            _userManager = userManager;
            _authorizationService = AuthorizationService;
        }

        #endregion

        #region HandleFunctions
        public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserClaimsResponse>("User Not Found");
            var result = await _authorizationService.ManageUserClaimData(user);
            return Success(result);
        }
        #endregion
    }
}
