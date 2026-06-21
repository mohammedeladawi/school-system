using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using SchoolProject.Data.CustomExceptions;
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

    public async Task<List<Student>> GetPaginatedStudentsAsync(int pageNumber, int pageSize, string? searchTerm = null, StudentOrderingEnum? orderBy = null)
    {
        var students = await _studentRepository.GetPaginatedStudentsAsync(pageNumber, pageSize, searchTerm, orderBy);
        return students;
    }

    public async Task<int> GetTotalStudentsCountAsync()
    {
        return await _studentRepository.GetTotalCountAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        return student;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _studentRepository.GetAllStudentsAsync();
    }

    public async Task AddAsync(Student student)
    {
        await _studentRepository.AddAsync(student);
    }

    public async Task UpdateAsync(Student student)
    {
        await _studentRepository.UpdateAsync(student);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        await _studentRepository.DeleteAsync(student);
    }

    public async Task<bool> IsStudentExistByIdAsync(int id)
    {
        return await _studentRepository.IsExistByIdAsync(id);
    }

    public async Task<bool> IsStudentNameEnExistAsync(string nameEn, int? excludedId = null)
    {
        return await _studentRepository.IsStudentNameEnExistAsync(nameEn, excludedId);
    }
    public async Task<bool> IsStudentNameArExistAsync(string nameAr, int? excludedId = null)
    {
        return await _studentRepository.IsStudentNameArExistAsync(nameAr, excludedId);
    }

    public async Task<bool> IsExistByIdAsync(int id)
    {
        return await _studentRepository.IsExistByIdAsync(id);
    }
}