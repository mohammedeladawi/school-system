using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations;

public class DepartmentService : IDepartmentService
{
    private IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public Task AddAsync(Department entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Department entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Department>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Department> GetByIdAsync(int id)
    {
        return _departmentRepository.GetDepartmentByIdAsync(id);
    }

    public Task UpdateAsync(Department entity)
    {
        throw new NotImplementedException();
    }
}