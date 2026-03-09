using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts;

public interface IStudentService
{
    public Task<List<Student>> GetAllStudentsAsync();

    public Task<Student> GetStudentByIdAsync(int id);
}