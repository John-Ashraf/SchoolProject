using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Response;

namespace School.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResponse>>
    {
        public string RoleId { get; set; }
    }
}
