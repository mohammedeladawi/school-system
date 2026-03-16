using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstracts;


public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    public Task<List<Student>> GetAllStudentsAsync();


}