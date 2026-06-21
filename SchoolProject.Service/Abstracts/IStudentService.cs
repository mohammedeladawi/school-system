using SchoolProject.Data.Entities;
using SchoolProject.Service.ServiceBases;
using StudentProject.Data.Enums;

namespace SchoolProject.Service.Abstracts;

public interface IStudentService : IGenericService<Student>
{
    public Task<List<Student>> GetPaginatedStudentsAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm = null,
        StudentOrderingEnum? orderBy = null);

    public Task<int> GetTotalStudentsCountAsync();

    public Task<bool> IsStudentExistByIdAsync(int id);

    public Task<bool> IsStudentNameEnExistAsync(string nameEn, int? excludedId = null);

    public Task<bool> IsStudentNameArExistAsync(string nameAr, int? excludedId = null);
}