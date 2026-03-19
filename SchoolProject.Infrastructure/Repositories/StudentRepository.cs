using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using StudentProject.Data.Enums;

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
        var studentsList = await _students.Include(s => s.Department)
                                          .ToListAsync();

        return studentsList;
    }

    public async Task<List<Student>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm = null, StudentOrderingEnum? orderBy = null)
    {
        var query = _students.Include(s => s.Department).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(s => s.Name.Contains(searchTerm));
        }

        switch (orderBy)
        {
            case StudentOrderingEnum.Id:
                query = query.OrderBy(s => s.Id);
                break;
            case StudentOrderingEnum.StudentName:
                query = query.OrderBy(s => s.Name);
                break;
            case StudentOrderingEnum.Address:
                query = query.OrderBy(s => s.Address);
                break;
            case StudentOrderingEnum.DepartmentName:
                query = query.OrderBy(s => s.Department.Name);
                break;
        }

        var pagintedStudents = await query.Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

        return pagintedStudents;
    }

    public async Task<bool> IsNameExistExceptIdAsync(string studentName, int id)
    {
        var isExist = await _students.AsNoTracking()
                            .Where(s => s.Name == studentName && s.Id != id)
                            .AnyAsync();
        return isExist;
    }

    public async Task<bool> IsNameExistAsync(string studentName)
    {
        var isExist = await _students.AsNoTracking()
                            .Where(s => s.Name == studentName)
                            .AnyAsync();
        return isExist;
    }
}