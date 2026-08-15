using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;


namespace SchoolProject.Application.Interfaces.Repositories;

public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
{
    Task<List<DepartmentStudentsCountView>> GetStudentsCountViewAsync();
}

