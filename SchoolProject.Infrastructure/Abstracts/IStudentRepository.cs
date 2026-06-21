using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;
using StudentProject.Data.Enums;

namespace SchoolProject.Infrastructure.Abstracts;


public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    public Task<Student?> GetStudentByIdAsync(int id);
    public Task<List<Student>> GetAllStudentsAsync(); // All students and include departments
    public Task<List<Student>> GetPaginatedStudentsAsync(int pageNumber, int pageSize, string? searchTerm = null, StudentOrderingEnum? orderBy = null);
    public Task<bool> IsStudentNameEnExistAsync(string studentNameEn, int? excludedId = null);
    public Task<bool> IsStudentNameArExistAsync(string studentNameAr, int? excludedId = null);
}

