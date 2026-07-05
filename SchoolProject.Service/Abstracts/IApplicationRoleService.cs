using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Service.Abstracts;

public interface IApplicationRoleService
{
    public Task CreateAsync(string roleName);
    public Task<bool> DoesExistByNameAsync(string roleName);
    public Task EditAsync(ApplicationRole role);
    public Task<ApplicationRole?> GetByIdAsync(int id);
    public Task DeleteAsync(ApplicationRole role);
    public Task<List<ApplicationRole>> GetAllAsync();
}