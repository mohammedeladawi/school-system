using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Core.Interfaces.IdentityServices;

public interface IRoleManager
{
    public Task CreateAsync(string roleName);
    public Task<bool> DoesExistByNameAsync(string roleName);
    public Task EditAsync(ApplicationRole role);
    public Task<ApplicationRole?> GetByIdAsync(int id);
    public Task DeleteAsync(ApplicationRole role);
    public Task<List<ApplicationRole>> GetAllAsync();

    public Task<bool> ValidateRolesExistAsync(string[] roleNames);
}