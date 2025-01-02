using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Response;

namespace School.Core.Features.Authorization.Queries.Models
{
    public class GetRoleListQuery : IRequest<Response<List<GetRolesListResponse>>>
    {

    }
}
