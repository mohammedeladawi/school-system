using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations;

public class DepartmentService : IDepartmentService
{
    #region Private Fields
    private IDepartmentRepository _departmentRepository;
    #endregion

    #region Constructors
    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    #endregion

    #region Public Methods
    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _departmentRepository.GetByIdAsync(id);
    }

    public async Task<bool> DoesExistByIdAsync(int id)
    {
        return await _departmentRepository.DoesExistByIdAsync(id);
    }

    public async Task<List<DepartmentStudentsCountView>> GetStudentsCountViewAsync()
    {
        return await _departmentRepository.GetStudentsCountViewAsync();
    }
    #endregion

}