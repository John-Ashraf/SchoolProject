using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Queries.Models;
using School.Services.Abstracts;
using SchoolProject.Core.Resources;

namespace School.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>,
         IRequestHandler<ConfirmEmailQuery, Response<string>>,
         IRequestHandler<ConfirmResetPasswordQuery, Response<string>>

    {


        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructors
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                            IAuthenticationService authenticationService)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }


        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>(result);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmEmail(request.UserId, request.Code);
            if (result == "ErrorWhenConfirmEmail" || result == "Useridisnotvalid" || result == "ErrorWhenConfirmEmail")
            {
                return BadRequest<string>(result);
            }
            return Success(result);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmResetPassword(request.Code, request.Email);
            if (result == "UserNotFound")
            {
                return NotFound<string>(result);
            }
            else if (result == "Failed")
            {
                return BadRequest<string>(result);
            }
            else
            {
                return Success(result);
            }
        }


        #endregion
    }
}
