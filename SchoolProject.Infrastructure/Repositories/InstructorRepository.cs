using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Bases;

namespace SchoolProject.Infrastructure.Repositories;

public class InstructorRepository :
    GenericRepositoryAsync<Instructor>,
    IInstructorRepository
{
    #region Private Fields
    private readonly DbSet<Instructor> _instructors;
    #endregion

    #region Constructors
    public InstructorRepository(AppDbContext context) : base(context)
    {
        _instructors = context.Instructors;
    }
    #endregion

    #region Private Methods
    private async Task<bool> IsInstructorNameExistAsync(
        Expression<Func<Instructor, bool>> filter,
        int? excludedId)
    {
        var query = _instructors.AsNoTracking().Where(filter);

        if (excludedId != null)
            query = query.Where(i => i.Id != excludedId.Value);

        return await query.AnyAsync();
    }
    #endregion

    #region Public Methods
    public async Task<bool> DoesNameEnExistAsync(
        string nameEn,
        int? excludedId = null)
    {
        return await IsInstructorNameExistAsync(i => i.NameEn == nameEn, excludedId);
    }

    public async Task<bool> DoesNameArExistAsync(
        string nameAr,
        int? excludedId = null)
    {
        return await IsInstructorNameExistAsync(i => i.NameAr == nameAr, excludedId);
    }
    #endregion
}
