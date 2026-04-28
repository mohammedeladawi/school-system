using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using StudentProject.Data.Enums;

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
        if (await _studentRepository.IsNameExistAsync(student.NameEn)) return false;

        await _studentRepository.AddAsync(student);

        return true;
    }

    public async Task<bool> EditStudentAsync(Student student)
    {
        if (await _studentRepository.IsNameExistExceptIdAsync(student.NameEn, student.Id))
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

    public async Task<List<Student>> GetPaginatedStudentsAsync(int pageNumber, int pageSize, string? searchTerm = null, StudentOrderingEnum? orderBy = null)
    {
        var students = await _studentRepository.GetPaginatedAsync(pageNumber, pageSize, searchTerm, orderBy);
        return students;
    }

    public async Task<int> GetTotalStudentsCountAsync()
    {
        return await _studentRepository.GetTotalCountAsync();
    }
}