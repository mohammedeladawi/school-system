using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.IdentityServices;

public class AuthorizationService : IAuthorizationService
{
    #region Fields
    private readonly IUserManager _userManager;
    private readonly UserManager<ApplicationUser> _UserManager;
    private readonly AppDbContext _dbContext;
    #endregion

    #region Constructors
    public AuthorizationService(
        IUserManager userManager,
        UserManager<ApplicationUser> UserManager,
        AppDbContext dbContext)
    {
        _userManager = userManager;
        _UserManager = UserManager;
        _dbContext = dbContext;
    }
    #endregion

    #region Public Methods
    public async Task<IList<string>> GetUserRolesAsync(int userId)
    {
        var user = await _userManager.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception($"User with ID {userId} not found.");
        }

        var userRoles = await _UserManager.GetRolesAsync(user);
        return userRoles;
    }

    public async Task UpdateUserPermissionClaims(ApplicationUser user, IList<string> permissionClaims)
    {
        var existingClaims = (await _UserManager.GetClaimsAsync(user))
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();

        var claimsToRemove = existingClaims.Except(permissionClaims)
            .Select(c => new Claim("Permission", c))
            .ToList();

        var claimsToAdd = permissionClaims.Except(existingClaims)
            .Select(c => new Claim("Permission", c))
            .ToList();

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var removeResult = await _UserManager.RemoveClaimsAsync(user, claimsToRemove);
            if (!removeResult.Succeeded)
                throw new Exception($"Failed to remove claims from user {user.UserName}: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");

            var addResult = await _UserManager.AddClaimsAsync(user, claimsToAdd);
            if (!addResult.Succeeded)
                throw new Exception($"Failed to add claims to user {user.UserName}: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");

            await _UserManager.UpdateSecurityStampAsync(user);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IList<string>> GetUserPermissionsAsync(int userId)
    {
        var user = await _userManager.GetByIdAsync(userId);
        var claims = await _UserManager.GetClaimsAsync(user);
        var permissions = claims.Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();

        return permissions;
    }

    public async Task UpdateUserRoles(ApplicationUser user, IList<string> roleNames)
    {
        var existingRoles = await _UserManager.GetRolesAsync(user);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var removeResult = await _UserManager.RemoveFromRolesAsync(user, existingRoles);
            if (!removeResult.Succeeded)
                throw new Exception($"Failed to remove roles from user {user.UserName}: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");

            var addResult = await _UserManager.AddToRolesAsync(user, roleNames);
            if (!addResult.Succeeded)
                throw new Exception($"Failed to add roles to user {user.UserName}: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");

            await _UserManager.UpdateSecurityStampAsync(user);

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