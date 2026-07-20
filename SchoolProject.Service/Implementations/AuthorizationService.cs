using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations;

public class AuthorizationService : IAuthorizationService
{
    #region Fields
    private readonly IApplicationUserService _applicationUserService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _dbContext;
    #endregion

    #region Constructors
    public AuthorizationService(
        IApplicationUserService applicationUserService,
        UserManager<ApplicationUser> userManager,
        AppDbContext dbContext)
    {
        _applicationUserService = applicationUserService;
        _userManager = userManager;
        _dbContext = dbContext;
    }
    #endregion

    #region Public Methods
    public async Task<IList<string>> GetUserRolesAsync(int userId)
    {
        var user = await _applicationUserService.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception($"User with ID {userId} not found.");
        }

        var userRoles = await _applicationUserService.GetUserRolesAsync(user);
        return userRoles;
    }

    public async Task UpdateUserRoles(ApplicationUser user, IList<string> roleNames)
    {
        var existingRoles = await _userManager.GetRolesAsync(user);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            if (!removeResult.Succeeded)
                throw new Exception($"Failed to remove roles from user {user.UserName}: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");

            var addResult = await _userManager.AddToRolesAsync(user, roleNames);
            if (!addResult.Succeeded)
                throw new Exception($"Failed to add roles to user {user.UserName}: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;

        }
    }
    #endregion
}