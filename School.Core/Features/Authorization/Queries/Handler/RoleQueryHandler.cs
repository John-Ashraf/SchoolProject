using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Models;
using School.Core.Features.Authorization.Queries.Response;
using School.Data.Entities.Identity;
using School.Services.Abstracts;
using SchoolProject.Core.Resources;

namespace School.Core.Features.Authorization.Queries.Handler
{
    public class RoleQueryHandler : ResponseHandler,
                                                IRequestHandler<GetRoleListQuery, Response<List<GetRolesListResponse>>>,
                                                IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
                                                IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResponse>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<AppUser> _userManager;
        #endregion
        #region Constructors
        public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                IAuthorizationService authorizationService,
                                IMapper mapper,
                                UserManager<AppUser> userManager)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        #endregion
        #region HandleFunction
        public async Task<Response<List<GetRolesListResponse>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
            var result = _mapper.Map<List<GetRolesListResponse>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleById(request.RoleId);
            if (role == null)
            {
                return NotFound<GetRoleByIdResponse>();
            }
            var result = _mapper.Map<IdentityRole, GetRoleByIdResponse>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound<ManageUserRolesResponse>("User Not Found");
            }
            var response = await _authorizationService.ManageUserRolesData(user);
            return Success(response);

        }
        #endregion
    }
}
