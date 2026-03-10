using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        this._studentRepository = studentRepository;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllStudentsAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {

        var student = await _studentRepository.GetTableNoTracking()
                            .Include(s => s.Department)
                            .Where(s => s.Id == id)
                            .FirstOrDefaultAsync();
        return student;
    }

    public async Task<bool> AddStudentAsync(Student student)
    {
        bool isExist = await _studentRepository.GetTableNoTracking()
                            .Where(s => s.Name == student.Name)
                            .AnyAsync();

        if (isExist) return false;

        // Add the student to the database
        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();

        return true;
    }
}