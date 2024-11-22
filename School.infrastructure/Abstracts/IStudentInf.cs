using School.Data.Entities;
using School.infrastructure.InfrastructureBases;

namespace School.infrastructure.Abstracts
{
    public interface IStudentInf : IGenericRepoAsync<Student>
    {
        public Task<List<Student>> GetStudentsAsync();
    }
}
