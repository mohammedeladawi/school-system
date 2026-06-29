using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Service.Implementations;

public class ApplicationRoleService : IApplicationRoleService
{
    #region Private Fields
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IStringLocalizer<SharedResource> _localizer;
    #endregion

    #region Constructor
    public ApplicationRoleService(
        RoleManager<ApplicationRole> roleManager,
        IStringLocalizer<SharedResource> localizer)
    {
        _roleManager = roleManager;
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

    #endregion
}