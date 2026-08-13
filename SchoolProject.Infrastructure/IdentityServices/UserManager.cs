using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Core.Interfaces.IdentityServices;
using Microsoft.Extensions.Logging;

namespace SchoolProject.Infrastructure.IdentityServices;

public class UserManager : IUserManager
{
    #region Private Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserManager> _logger;
    #endregion

    #region Constructor
    public UserManager(UserManager<ApplicationUser> UserManager, ILogger<UserManager> logger)
    {
        _userManager = UserManager;
        _logger = logger;
    }

    #endregion

    #region Private Methods
    private async Task<bool> DoesExistAsync(
        Expression<Func<ApplicationUser, bool>> predicate,
        int? excludeUserId = null)
    {
        var query = _userManager.Users.Where(predicate);
        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId.Value);

        return await query.AnyAsync();
    }

    #endregion

    #region Public Methods
    public async Task AddAsync(ApplicationUser user, string password)
    {
        var createResult = await _userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            throw new Exception(string.Join(" ", createResult.Errors.Select(e => e.Description)));

        // Todo: Change the default role 
        var addToRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
    }

    public async Task<bool> DoesEmailExist(string email, int? excludeUserId = null)
    {
        return await DoesExistAsync(u => u.Email == email, excludeUserId);
    }

    public async Task<bool> DoesUserNameExist(string userName, int? excludeUserId = null)
    {
        return await DoesExistAsync(u => u.UserName == userName, excludeUserId);
    }

    public async Task<ApplicationUser?> GetByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return user;
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<ApplicationUser?> GetByUserNameAndPasswordAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return null;

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid) return null;

        return user;
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        var updatedUser = await _userManager.UpdateAsync(user);
        if (!updatedUser.Succeeded)
            throw new Exception(string.Join(" ", updatedUser.Errors.Select(e => e.Description)));
    }

    public async Task DeleteAsync(ApplicationUser user)
    {
        var deleteResult = await _userManager.DeleteAsync(user);
        if (!deleteResult.Succeeded)
            throw new Exception(string.Join(" ", deleteResult.Errors.Select(e => e.Description)));
    }

    public Task<bool> DoesExistByIdAsync(int id)
    {
        var isExist = DoesExistAsync(u => u.Id == id);
        return isExist;
    }

    public async Task<int> GetTotalCountAsync()
    {
        int usersCount = await _userManager.Users.CountAsync();
        return usersCount;
    }

    public async Task<List<ApplicationUser>> GetPaginatedListAsync(int pageNumber, int pageSize)
    {
        var paginatedApplicationUsers = await _userManager.Users.OrderBy(au => au.Id)
                                      .Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

        return paginatedApplicationUsers;
    }

    public async Task ChangePasswordAsync(int id, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            _logger.LogError("User is not found");
            return;
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!changePasswordResult.Succeeded)
            throw new Exception(string.Join(" ", changePasswordResult.Errors.Select(e => e.Description)));
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }


    public async Task ConfirmEmailAsync(ApplicationUser user, string token)
    {
        var confirmationResult = await _userManager.ConfirmEmailAsync(user, token);

        if (!confirmationResult.Succeeded)
            throw new Exception(string.Join(" ", confirmationResult.Errors.Select(e => e.Description)));
    }

    #endregion
}