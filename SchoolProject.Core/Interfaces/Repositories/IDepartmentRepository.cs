using SchoolProject.Core.Interfaces.Bases;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;


namespace SchoolProject.Core.Interfaces.Repositories;

public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
{
    Task<Department?> GetByIdAsync(int id);
    Task<List<DepartmentStudentsCountView>> GetStudentsCountViewAsync();
}

