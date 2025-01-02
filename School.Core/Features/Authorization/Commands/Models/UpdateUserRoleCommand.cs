using MediatR;
using School.Core.Bases;
using School.Data.Requests;

namespace School.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRoleCommand : UpdateUserRolesRequest, IRequest<Response<string>>
    {

    }
}
