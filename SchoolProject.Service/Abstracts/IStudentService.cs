using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts;

public interface IStudentService
{
    public Task<List<Student>> GetAllStudentsAsync();

    public Task<Student> GetStudentByIdAsync(int id);

    public Task<bool> AddStudentAsync(Student student);

    public Task<bool> EditStudentAsync(Student student);

    public Task<bool> DeleteByIdAsync(int id);

    public Task<List<Student>> GetPaginatedStudentsAsync(int pageNumber, int pageSize, string? searchTerm = null, string[]? orderBy = null);

    public Task<int> GetTotalStudentsCountAsync();


}