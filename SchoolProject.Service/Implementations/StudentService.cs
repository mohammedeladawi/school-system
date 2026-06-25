using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using StudentProject.Data.Enums;

namespace SchoolProject.Service.Implementations;

public class StudentService : IStudentService
{
    #region Private Fields
    private readonly IStudentRepository _studentRepository;
    #endregion

    #region Constructor
    public StudentService(IStudentRepository studentRepository)
    {
        this._studentRepository = studentRepository;
    }
    #endregion

    #region Public Methods
    public async Task<List<Student>> GetPaginatedListAsync(int pageNumber, int pageSize, string? searchTerm = null, StudentOrderingEnum? orderBy = null)
    {
        var students = await _studentRepository.GetPaginatedListAsync(pageNumber, pageSize, searchTerm, orderBy);
        return students;
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        return student;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task AddAsync(Student student)
    {
        await _studentRepository.AddAsync(student);
    }

    public async Task UpdateAsync(Student student)
    {
        await _studentRepository.UpdateAsync(student);
    }

    public async Task DeleteAsync(Student student)
    {
        await _studentRepository.DeleteAsync(student);
    }

    public async Task<bool> DoesNameEnExistAsync(string nameEn, int? excludedId = null)
    {
        return await _studentRepository.DoesNameEnExistAsync(nameEn, excludedId);
    }

    public async Task<bool> DoesNameArExistAsync(string nameAr, int? excludedId = null)
    {
        return await _studentRepository.DoesNameArExistAsync(nameAr, excludedId);
    }

    public async Task<bool> DoesExistByIdAsync(int id)
    {
        return await _studentRepository.DoesExistByIdAsync(id);
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _studentRepository.GetTotalCountAsync();
    }
    #endregion
}