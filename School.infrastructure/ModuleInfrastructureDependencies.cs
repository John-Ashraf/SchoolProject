using Microsoft.Extensions.DependencyInjection;
using School.infrastructure.Abstracts;
using School.infrastructure.InfrastructureBases;
using School.infrastructure.Repositories;

namespace School.infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddinfrastructureDependencies(this IServiceCollection services)
        {
            _ = services.AddTransient<IStudentInf, StudentRepo>();
            _ = services.AddTransient<IDepartmentInf, DepartmentRepo>();
            _ = services.AddTransient<ISubjectInf, SubjectRepo>();
            _ = services.AddTransient<IInstructorInf, InstructorRepo>();
            _ = services.AddTransient(typeof(IGenericRepoAsync<>), typeof(GenericRepoAsync<>));
            return services;
        }
    }
}
