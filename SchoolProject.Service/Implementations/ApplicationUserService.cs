using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Service.Implementations;

public class ApplicationUserService : IApplicationUserService
{
    #region Private Fields
    private readonly UserManager<ApplicationUser> _userManager;
    #endregion

    #region Constructor
    public ApplicationUserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    #endregion

    #region Private Methods
    private async Task<bool> IsExistAsync(
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

        var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
    }

    public async Task<bool> DoesEmailExist(string email, int? excludeUserId = null)
    {
        return await IsExistAsync(u => u.Email == email, excludeUserId);
    }

    public async Task<bool> DoesUserNameExist(string userName, int? excludeUserId = null)
    {
        return await IsExistAsync(u => u.UserName == userName, excludeUserId);
    }

    public async Task<ApplicationUser?> GetByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
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
        var isExist = IsExistAsync(u => u.Id == id);
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
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!changePasswordResult.Succeeded)
            throw new Exception(string.Join(" ", changePasswordResult.Errors.Select(e => e.Description)));
    }

    public async Task<ApplicationUser?> GetByUserNameAndPasswordAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return null;

        var isValid = await _userManager.CheckPasswordAsync(user, password);
        return isValid ? user : null;
    }

    public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
    {
        return (await _userManager.GetRolesAsync(user)).ToList();
    }


    #endregion
}