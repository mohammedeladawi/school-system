using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstracts;

public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
{
    Task<List<DepartmentStudentsCountView>> GetStudentsCountViewAsync();
}

