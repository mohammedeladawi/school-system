using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

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
        _departments = context.Set<Department>();
    }
    #endregion

    #region Public Methods
    public override async Task<Department?> GetByIdAsync(int id)
    {
        var department =
                await _departments
                        .AsNoTracking()
                        .Where(d => d.Id == id)
                        .Include(d => d.Instructors)
                        .Include(d => d.Students)
                        .Include(d => d.Subjects)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync();

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
