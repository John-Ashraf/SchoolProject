using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using School.Services.AuthServices.Interfaces;

namespace School.Api.Filters
{
    public class AuthFilter : IAsyncActionFilter
    {
        private readonly ICurrentUser _currentUserService;
        public AuthFilter(ICurrentUser currentUser)
        {
            _currentUserService = currentUser;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == true)
            {
                var roles = await _currentUserService.GetCurrentUserRolesAsync();
                if (roles.All(x => x != "User"))
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
                else
                {
                    _ = await next();
                }

            }
        }
    }
}
