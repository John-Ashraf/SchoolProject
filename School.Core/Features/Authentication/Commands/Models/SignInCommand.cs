using MediatR;
using School.Core.Bases;
using School.Data.Helpers;

namespace School.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JWTAuthRes>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
