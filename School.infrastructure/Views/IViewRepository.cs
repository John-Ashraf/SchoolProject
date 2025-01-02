using School.infrastructure.InfrastructureBases;

namespace School.infrastructure.Views
{
    public interface IViewRepository<T> : IGenericRepoAsync<T> where T : class
    {
    }
}
