using Microsoft.Extensions.DependencyInjection;
using School.Services.Abstracts;
using School.Services.Implementation;

namespace School.Services
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            _ = services.AddTransient<IStudentService, StudentService>();
            _ = services.AddTransient<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}
