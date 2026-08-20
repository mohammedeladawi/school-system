using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Application.Interfaces.IdentityServices;
using Microsoft.Extensions.Logging;
using SchoolProject.Application.Helpers;
using SchoolProject.Domain.Entities;
using SchoolProject.Application.Interfaces.Bases;

namespace SchoolProject.Infrastructure.Bases;

public class GenericIdentityUserManagerAsync<TUser> :
    IGenericIdentityUserManagerAsync<TUser>
    where TUser : ApplicationUser
{
    #region Private Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<GenericIdentityUserManagerAsync<TUser>> _logger;
    #endregion

    #region Constructor
    public GenericIdentityUserManagerAsync(
        UserManager<ApplicationUser> UserManager,
        ILogger<GenericIdentityUserManagerAsync<TUser>> logger)
    {
        _userManager = UserManager;
        _logger = logger;
    }

    #endregion

    #region Private Methods
    private async Task<bool> DoesExistAsync(
        Expression<Func<TUser, bool>> predicate,
        int? excludeUserId = null)
    {
        var query = _userManager.Users.OfType<TUser>().Where(predicate);
        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId.Value);

        return await query.AnyAsync();
    }

    #endregion

    #region Public Methods
    public async Task AddAsync(
        TUser user,
        string password,
        string role)
    {
        var createResult = await _userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(createResult.Errors));

        var addToRoleResult = await _userManager.AddToRoleAsync(user, role);
        if (!addToRoleResult.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(addToRoleResult.Errors));
    }


    public async Task<bool> DoesEmailExist(string email, int? excludeUserId = null)
    {
        return await DoesExistAsync(u => u.Email == email, excludeUserId);
    }

    public async Task<bool> DoesUserNameExist(string userName, int? excludeUserId = null)
    {
        return await DoesExistAsync(u => u.UserName == userName, excludeUserId);
    }

    public async Task<TUser?> GetByIdAsync(
        int id,
        Expression<Func<TUser, object>>[]? includes = null)
    {
        IQueryable<TUser> query = _userManager.Users.OfType<TUser>();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        var user = await query.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<TUser?> GetByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return (TUser?)user;
    }

    public async Task<TUser?> GetByUserNameAndPasswordAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return null;

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid) return null;

        return (TUser)user;
    }

    public async Task UpdateAsync(TUser user)
    {
        var updatedUser = await _userManager.UpdateAsync(user);

        if (!updatedUser.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(updatedUser.Errors));
    }

    public async Task DeleteAsync(TUser user)
    {
        var deleteResult = await _userManager.DeleteAsync(user);
        if (!deleteResult.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(deleteResult.Errors));
    }

    public Task<bool> DoesExistByIdAsync(int id)
    {
        var isExist = DoesExistAsync(u => u.Id == id);
        return isExist;
    }

    public async Task<int> GetTotalCountAsync()
    {
        int usersCount = await _userManager.Users.OfType<TUser>().CountAsync();
        return usersCount;
    }

    public async Task<List<TUser>> GetAllAsync(
           Expression<Func<TUser, object>>[]? includes = null,
           Expression<Func<TUser, bool>>? filter = null,
           bool asNoTracking = true
           )
    {
        IQueryable<TUser> query = _userManager.Users.OfType<TUser>();

        if (filter != null)
            query = query.Where(filter);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public Task<List<TUser>> GetPaginatedListAsync(
        int pageNumber = 1,
        int pageSize = 10,
         Expression<Func<TUser, object>>[]? includes = null,
         Expression<Func<TUser, bool>>? filter = null,
         bool asNoTracking = true)
    {
        IQueryable<TUser> query = _userManager.Users.OfType<TUser>().OrderBy(au => au.Id);

        if (filter != null)
            query = query.Where(filter);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (asNoTracking)
            query = query.AsNoTracking();

        return query.Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    }

    public async Task<bool> CheckPasswordAsync(int id, string password)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return user is not null && await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task ChangePasswordAsync(TUser user, string newPassword)
    {
        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user!);

        var result = await _userManager.ResetPasswordAsync(user!, resetToken, newPassword);
        if (!result.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(result.Errors));
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
            throw new Exception(Utils.IdentityErrorsFormater(changePasswordResult.Errors));
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(TUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }


    public async Task ConfirmEmailAsync(TUser user, string token)
    {
        var confirmationResult = await _userManager.ConfirmEmailAsync(user, token);

        if (!confirmationResult.Succeeded)
            throw new Exception(Utils.IdentityErrorsFormater(confirmationResult.Errors));
    }

    #endregion
}