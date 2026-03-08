using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories;

public class StudentRepository :
    GenericRepositoryAsync<Student>, IStudentRepository
{
    private readonly DbSet<Student> _students;

    public StudentRepository(AppDbContext context) : base(context)
    {
        _students = context.Set<Student>();
    }
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await
         _students.Include(s => s.Department)
            .ToListAsync();
    }
}