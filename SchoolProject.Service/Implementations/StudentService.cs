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
        if (await _IsNameExistsAsync(student.Name)) return false;

        // Add the student to the database
        await _studentRepository.AddAsync(student);

        return true;
    }

    private async Task<bool> _IsNameExistsAsync(string name)
    {
        var isExist = await _studentRepository.GetTableNoTracking()
                            .Where(s => s.Name == name)
                            .AnyAsync();
        return isExist;
    }

    private async Task<bool> _IsNameExistsExceptIdAsync(string name, int id)
    {
        var isExist = await _studentRepository.GetTableNoTracking()
                            .Where(s => s.Name == name && s.Id != id)
                            .AnyAsync();
        return isExist;
    }

    public async Task<bool> EditStudentAsync(Student student)
    {
        if (await _IsNameExistsExceptIdAsync(student.Name, student.Id))
            return false;

        await _studentRepository.UpdateAsync(student);
        return true;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null) return false;

        try
        {
            await _studentRepository.DeleteAsync(student);
            return true;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error while deleting student with id {id}");
        }

    }
}