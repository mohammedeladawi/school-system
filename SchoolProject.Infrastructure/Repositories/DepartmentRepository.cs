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

}
