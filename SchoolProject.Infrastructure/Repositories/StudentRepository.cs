using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using StudentProject.Data.Enums;

namespace SchoolProject.Infrastructure.Repositories;

public class StudentRepository :
    GenericRepositoryAsync<Student>,
    IStudentRepository
{
    private readonly DbSet<Student> _students;

    public StudentRepository(AppDbContext context) : base(context)
    {
        _students = context.Set<Student>();
    }

    #region Private Methods
    private async Task<bool> IsStudentNameExistAsync(
    Expression<Func<Student, bool>> expression,
    int? excludedId)
    {
        var query = _students.AsNoTracking().Where(expression);

        if (excludedId != null)
            query = query.Where(s => s.Id != excludedId.Value);

        return await query.AnyAsync();
    }
    #endregion


    #region Public Methods
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        var studentsList = await _students.Include(s => s.Department)
                                          .ToListAsync();
        return studentsList;
    }

    public async Task<List<Student>> GetPaginatedStudentsAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm = null,
        StudentOrderingEnum? orderBy = null)
    {
        var query = _students.Include(s => s.Department).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(s => s.NameEn.Contains(searchTerm) || s.NameAr.Contains(searchTerm));
        }

        switch (orderBy)
        {
            case StudentOrderingEnum.Id:
                query = query.OrderBy(s => s.Id);
                break;
            case StudentOrderingEnum.StudentName:
                query = query.OrderBy(s => s.NameEn);
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

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _students.AsNoTracking()
                              .Include(s => s.Department)
                              .Where(s => s.Id == id)
                              .FirstOrDefaultAsync();
    }


    public async Task<bool> IsStudentNameEnExistAsync(
        string studentNameEn,
        int? excludedId = null)
    {
        return await IsStudentNameExistAsync(s => s.NameEn == studentNameEn, excludedId);
    }

    public async Task<bool> IsStudentNameArExistAsync(
        string studentNameAr,
        int? excludedId = null)
    {
        return await IsStudentNameExistAsync(s => s.NameEn == studentNameAr, excludedId);
    }
    #endregion

}
