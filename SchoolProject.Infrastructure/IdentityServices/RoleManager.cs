using Microsoft.AspNetCore.Identity;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;

namespace SchoolProject.Infrastructure.IdentityServices;

public class RoleManager : IRoleManager
{
    #region Private Fields
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _UserManager;
    #endregion

    #region Constructor
    public RoleManager(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> UserManager)
    {
        _roleManager = roleManager;
        _UserManager = UserManager;
    }
    #endregion

    #region Public Methods

    public async Task CreateAsync(string roleName)
    {
        var result = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
        if (!result.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(result.Errors));
    }

    public Task<bool> DoesExistByNameAsync(string roleName)
    {
        return _roleManager.RoleExistsAsync(roleName);
    }

    public async Task EditAsync(ApplicationRole role)
    {
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(result.Errors));
    }

    public Task<ApplicationRole?> GetByIdAsync(int id)
    {
        return _roleManager.FindByIdAsync(id.ToString());
    }

    public async Task DeleteAsync(ApplicationRole role)
    {
        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(result.Errors));
    }

    public async Task<List<ApplicationRole>> GetAllAsync()
    {
        return _roleManager.Roles.ToList();
    }

    public async Task<bool> ValidateRolesExistAsync(string[] roleNames)
    {
        var allRoles = await GetAllAsync();
        var existingRoleNames = allRoles.Select(r => r.Name).ToList();
        return roleNames.All(roleName => existingRoleNames.Contains(roleName));
    }

    public async Task<bool> IsRoleInUseAsync(string roleName)
    {
        var usersInRole = await _UserManager.GetUsersInRoleAsync(roleName);
        return usersInRole.Any();
    }

    #endregion
}