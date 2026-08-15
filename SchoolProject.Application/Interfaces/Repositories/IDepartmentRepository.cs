using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Views;


namespace SchoolProject.Application.Interfaces.Repositories;

public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
{
    Task<List<DepartmentStudentsCountView>> GetStudentsCountViewAsync();
}

