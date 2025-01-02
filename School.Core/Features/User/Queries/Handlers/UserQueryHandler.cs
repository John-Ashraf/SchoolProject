using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Core.Bases;

using School.Core.Features.User.Queries.Models;
using School.Core.Features.User.Queries.Response;
using School.Core.Wrappers;
using School.Data.Entities.Identity;

namespace School.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler, IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserListResponse>>
                                                    , IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region fields
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        #endregion
        #region constructor
        public UserQueryHandler(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }


        #endregion
        #region Handle Functions
        async Task<PaginatedResult<GetUserListResponse>> IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserListResponse>>.Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUserListResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                return NotFound<GetUserByIdResponse>("User is not Exist");
            }
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success<GetUserByIdResponse>(result);
        }

        #endregion



    }
}
