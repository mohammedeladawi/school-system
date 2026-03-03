using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Abstracts;


public interface IStudentRepository
{
    public Task<List<Student>> GetAllStudentsAsync();
}