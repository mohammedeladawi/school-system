using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Bases;

namespace SchoolProject.Infrastructure.Repositories;

public class DepartmentRepository :
    GenericRepositoryAsync<Department>,
    IDepartmentRepository
{
    #region Private Fields
    private readonly DbSet<Department> _departments;
    #endregion

    #region Constructors
    public DepartmentRepository(AppDbContext context) : base(context)
    {
        _departments = context.Departments;
    }
    #endregion

    #region Public Methods

    public override async Task<Department?> GetByIdAsync(
        int id,
        Expression<Func<Department, object>>[]? includes = null,
        bool asNoTracking = true)
    {
        var query = _departments.AsQueryable();
        query = query.Where(d => d.Id == id);

        foreach (var include in includes)
            query = query.Include(include);

        if (asNoTracking)
            query = query.AsNoTracking();

        var department = await query.AsSplitQuery().FirstOrDefaultAsync();
        return department;
    }

    public async Task<List<DepartmentStudentsCountView>> GetStudentsCountViewAsync()
    {
        return await _dbContext.Set<DepartmentStudentsCountView>()
            .AsNoTracking()
            .OrderBy(x => x.DepartmentId)
            .ToListAsync();
    }

    #endregion
}
