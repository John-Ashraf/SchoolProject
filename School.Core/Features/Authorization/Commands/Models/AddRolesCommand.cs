using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authorization.Commands.Models
{
    public class AddRolesCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
