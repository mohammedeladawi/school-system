using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Bases;
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
        _students = context.Students;
    }
    #endregion

    #region Private Methods
    private async Task<bool> IsStudentNameExistAsync(
        Expression<Func<Student, bool>> filter,
        int? excludedId)
    {
        var query = _students.AsNoTracking().Where(filter);

        if (excludedId != null)
            query = query.Where(s => s.Id != excludedId.Value);

        return await query.AnyAsync();
    }
    #endregion

    #region Public Methods
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
        return await IsStudentNameExistAsync(s => s.NameAr == studentNameAr, excludedId);
    }
    #endregion
}
