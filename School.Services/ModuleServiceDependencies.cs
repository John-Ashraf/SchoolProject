using Microsoft.Extensions.DependencyInjection;
using School.Services.Abstracts;
using School.Services.AuthServices.Implementations;
using School.Services.AuthServices.Interfaces;
using School.Services.Implementation;

namespace School.Services
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            _ = services.AddTransient<IStudentService, StudentService>();
            _ = services.AddTransient<IDepartmentService, DepartmentService>();
            _ = services.AddTransient<IAuthenticationService, AuthenticationService>();
            _ = services.AddTransient<IAuthorizationService, AuthorizationService>();
            _ = services.AddTransient<IEmailService, EmailService>();
            _ = services.AddTransient<IApplicationUserService, ApplicationUserService>();
            _ = services.AddTransient<ICurrentUser, CurrentUserServices>();
            _ = services.AddTransient<IInstructorService, InstructorService>();
            _ = services.AddTransient<IFileService, FileService>();

            return services;
        }
    }
}
