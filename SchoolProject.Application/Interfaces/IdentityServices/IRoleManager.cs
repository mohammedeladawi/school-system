using SchoolProject.Domain.Entities.Identities;

namespace SchoolProject.Application.Interfaces.IdentityServices;

public interface IRoleManager
{
    public Task CreateAsync(string roleName);
    public Task<bool> DoesExistByNameAsync(string roleName);
    public Task EditAsync(ApplicationRole role);
    public Task<ApplicationRole?> GetByIdAsync(int id);
    public Task DeleteAsync(ApplicationRole role);
    public Task<List<ApplicationRole>> GetAllAsync();

    public Task<bool> IsRoleInUseAsync(string roleName);
    public Task<bool> ValidateRolesExistAsync(string[] roleNames);
}