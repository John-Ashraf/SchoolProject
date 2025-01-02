using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Commands.Models;
using School.Services.Abstracts;
using SchoolProject.Core.Resources;

namespace School.Core.Features.Authorization.Commands.Handler
{
    public class ClaimsCommandHandler : ResponseHandler, IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;

        #endregion
        #region Constructors
        public ClaimsCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                    IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        #endregion
        #region Functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(result);
                case "FailedToRemoveOldClaims": return BadRequest<string>(result);
                case "FailedToAddNewClaims": return BadRequest<string>(result);
                case "FailedToUpdateClaims": return BadRequest<string>(result);
            }
            return Success<string>(result);
        }
        #endregion
    }
}
