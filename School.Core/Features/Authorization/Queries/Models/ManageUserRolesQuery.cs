using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Response;

namespace School.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResponse>>
    {
        public string UserId { get; set; }
    }
}
