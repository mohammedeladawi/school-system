using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;
using StudentProject.Data.Enums;

namespace SchoolProject.Infrastructure.Abstracts;


public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    public Task<List<Student>> GetAllAsync();
    public Task<List<Student>> GetPaginatedListAsync(int pageNumber, int pageSize, string? searchTerm = null, StudentOrderingEnum? orderBy = null);
    public Task<bool> DoesNameEnExistAsync(string studentNameEn, int? excludedId = null);
    public Task<bool> DoesNameArExistAsync(string studentNameAr, int? excludedId = null);
}

