using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;
using StudentProject.Data.Enums;

namespace SchoolProject.Infrastructure.Abstracts;


public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    public Task<List<Student>> GetAllStudentsAsync();

    public Task<List<Student>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm = null, StudentOrderingEnum? orderBy = null);

    public Task<bool> IsNameExistAsync(string studentName);
    public Task<bool> IsNameExistExceptIdAsync(string studentName, int id);
}