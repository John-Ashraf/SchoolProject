using MediatR;
using School.Core.Features.User.Queries.Response;
using School.Core.Wrappers;

namespace School.Core.Features.User.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
