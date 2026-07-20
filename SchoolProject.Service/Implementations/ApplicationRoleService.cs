using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Service.Implementations;

public class ApplicationRoleService : IApplicationRoleService
{
    #region Private Fields
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<SharedResource> _localizer;
    #endregion

    #region Constructor
    public ApplicationRoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<SharedResource> localizer)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _localizer = localizer;
    }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods

    public async Task CreateAsync(string roleName)
    {
        var result = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
        if (!result.Succeeded)
        {
            var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception(errorMessage);
        }
    }

    public Task<bool> DoesExistByNameAsync(string roleName)
    {
        return _roleManager.RoleExistsAsync(roleName);
    }

    public async Task EditAsync(ApplicationRole role)
    {
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception(errorMessage);
        }
    }

    public Task<ApplicationRole?> GetByIdAsync(int id)
    {
        return _roleManager.FindByIdAsync(id.ToString());
    }

    public async Task DeleteAsync(ApplicationRole role)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
        if (usersInRole.Any())
            throw new DomainException(_localizer[SharedResourceKeys.RoleHasUsers]);

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception(errorMessage);
        }
    }

    public async Task<List<ApplicationRole>> GetAllAsync()
    {
        return _roleManager.Roles.ToList();
    }

    public async Task<bool> ValidateRolesExistAsync(List<string> roleNames)
    {
        var allRoles = await GetAllAsync();
        var existingRoleNames = allRoles.Select(r => r.Name).ToList();
        return roleNames.All(roleName => existingRoleNames.Contains(roleName));
    }

    #endregion
}