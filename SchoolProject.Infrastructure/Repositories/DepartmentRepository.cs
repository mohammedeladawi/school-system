using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories;

public class DepartmentRepository :
    GenericRepositoryAsync<Department>,
    IDepartmentRepository
{
    private readonly DbSet<Department> _departments;

    public DepartmentRepository(AppDbContext context) : base(context)
    {
        _departments = context.Set<Department>();
    }

    public async Task<Department?> GetDepartmentByIdAsync(int id)
    {
        return await _departments
                        .AsNoTracking()
                        .Where(d => d.Id == id)
                        .Include(d => d.Instructors)
                        .Include(d => d.Students)
                        .Include(d => d.Subjects)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync();
    }
}
