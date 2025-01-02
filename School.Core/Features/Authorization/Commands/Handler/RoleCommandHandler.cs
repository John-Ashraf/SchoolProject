using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Commands.Models;
using School.Services.Abstracts;

namespace School.Core.Features.Authorization.Commands.Handler
{
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRolesCommand, Response<string>>
                                                     , IRequestHandler<EditRoleCommand, Response<string>>
                                                     , IRequestHandler<DeleteRoleCommand, Response<string>>
                                                     , IRequestHandler<UpdateUserRoleCommand, Response<string>>
    {
        #region Fields

        IAuthorizationService _authorizationService;
        #endregion
        #region Constructor
        public RoleCommandHandler(IAuthorizationService authorizationService)
        {

            _authorizationService = authorizationService;
        }
        #endregion
        #region Functions
        public async Task<Response<string>> Handle(AddRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success")
            {
                return Success(result);
            }
            else
            {
                return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "Success")
            {
                return Success(result);
            }
            else if (result == "NotFound")
            {
                return NotFound<string>(result);
            }
            else
            {
                return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound")
            {
                return NotFound<string>(result);
            }
            else if (result == "Used")
            {
                return BadRequest<string>(result);
            }
            else if (result == "Success")
            {
                return Success<string>(result);
            }
            else
            {
                return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRole(request);
            if (result == "UserNotFound") return NotFound<string>(result);
            else if (result == "Failed To RemoveOldRoles" || result == "Failed To AddNewRoles" || result == "Failed To UpdateUserRoles") return BadRequest<string>(result);
            else
            {
                return Success(result);
            }
        }
        #endregion
    }
}
