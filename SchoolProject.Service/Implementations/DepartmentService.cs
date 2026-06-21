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

    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Department>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _departmentRepository.GetDepartmentByIdAsync(id);
    }

    public Task UpdateAsync(Department entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsExistByIdAsync(int id)
    {
        return await _departmentRepository.IsExistByIdAsync(id);
    }
}