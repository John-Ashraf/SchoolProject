using MediatR;
using School.Core.Bases;
using School.Data.DTOS;

namespace School.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResponse>>
    {
        public string UserId { get; set; }
    }
}
