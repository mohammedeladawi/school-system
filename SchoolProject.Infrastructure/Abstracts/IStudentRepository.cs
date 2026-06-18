using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;
using StudentProject.Data.Enums;

namespace SchoolProject.Infrastructure.Abstracts;


public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    public Task<List<Student>> GetAllStudentsAsync(); // All students and include departments
    public Task<List<Student>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm = null, StudentOrderingEnum? orderBy = null);
    public Task<bool> IsNameExistAsync(string studentNameEn, string studentNameAr);
    public Task<bool> IsNameExistExceptIdAsync(string studentNameEn, string studentNameAr, int id);
}

