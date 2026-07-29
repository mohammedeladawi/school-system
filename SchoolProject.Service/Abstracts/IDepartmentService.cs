using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Service.Abstracts;

public interface IDepartmentService
{
    public Task<Department?> GetByIdAsync(int id);
    public Task<bool> DoesExistByIdAsync(int id);
    public Task<List<DepartmentStudentsCountView>> GetStudentsCountViewAsync();
}