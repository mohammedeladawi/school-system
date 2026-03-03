using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }
}