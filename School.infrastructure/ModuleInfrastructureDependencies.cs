using Microsoft.Extensions.DependencyInjection;
using School.Data.Entities.Views;
using School.infrastructure.Abstracts;
using School.infrastructure.InfrastructureBases;
using School.infrastructure.Repositories;
using School.infrastructure.Repositories.Views;
using School.infrastructure.Views;

namespace School.infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddinfrastructureDependencies(this IServiceCollection services)
        {
            _ = services.AddTransient<IStudentInf, StudentRepo>();
            _ = services.AddTransient<IDepartmentInf, DepartmentRepo>();
            _ = services.AddTransient<ISubjectInf, SubjectRepo>();
            _ = services.AddTransient<IRefreshTokenInf, RefreshTokenRepo>();
            _ = services.AddTransient<IInstructorInf, InstructorRepo>();
            _ = services.AddTransient(typeof(IGenericRepoAsync<>), typeof(GenericRepoAsync<>));
            _ = services.AddTransient<IViewRepository<DepartmentView>, ViewDepartmentRepository>();
            return services;
        }
    }
}
