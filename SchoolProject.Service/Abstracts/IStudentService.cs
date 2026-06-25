using SchoolProject.Data.Entities;
using StudentProject.Data.Enums;

namespace SchoolProject.Service.Abstracts;

public interface IStudentService
{
    public Task<List<Student>> GetAllAsync();
    public Task AddAsync(Student student);
    public Task<Student?> GetByIdAsync(int id);
    public Task UpdateAsync(Student student);
    public Task DeleteAsync(Student student);
    public Task<bool> DoesExistByIdAsync(int id);
    public Task<int> GetTotalCountAsync();
    public Task<List<Student>> GetPaginatedListAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm = null,
        StudentOrderingEnum? orderBy = null);
    public Task<bool> DoesNameEnExistAsync(string nameEn, int? excludedId = null);
    public Task<bool> DoesNameArExistAsync(string nameAr, int? excludedId = null);
}