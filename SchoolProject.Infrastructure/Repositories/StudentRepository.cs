using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using StudentProject.Data.Enums;

namespace SchoolProject.Infrastructure.Repositories;

public class StudentRepository :
    GenericRepositoryAsync<Student>,
    IStudentRepository
{
    #region Private Fields
    private readonly DbSet<Student> _students;
    #endregion

    #region Constructors
    public StudentRepository(AppDbContext context) : base(context)
    {
        _students = context.Set<Student>();
    }
    #endregion

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
    public async Task<List<Student>> GetAllWithDepartmentAsync()
    {
        var studentsList = await _students.Include(s => s.Department)
                                          .ToListAsync();
        return studentsList;
    }

    public async Task<List<Student>> GetPaginatedListAsync(
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


    public async Task<bool> DoesNameEnExistAsync(
        string studentNameEn,
        int? excludedId = null)
    {
        return await IsStudentNameExistAsync(s => s.NameEn == studentNameEn, excludedId);
    }

    public async Task<bool> DoesNameArExistAsync(
        string studentNameAr,
        int? excludedId = null)
    {
        return await IsStudentNameExistAsync(s => s.NameEn == studentNameAr, excludedId);
    }
    #endregion
}
