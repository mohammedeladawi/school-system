using System.Linq.Expressions;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations;

public class ApplicationUserService : IApplicationUserService
{
    #region Private Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _dbContext;
    private readonly IEmailService _emailService;
    #endregion

    #region Constructor
    public ApplicationUserService(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        AppDbContext dbContext)
    {
        _userManager = userManager;
        _emailService = emailService;
        _dbContext = dbContext;
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

    private async Task SendConfirmationEmailAsync(ApplicationUser user, string token, string confirmationUrlTemplate)
    {
        var confirmationUrl = string.Format(confirmationUrlTemplate, user.Id, token);
        var emailSubject = "Confirm your email";
        var emailBody = $"""
                <h1>Welcome {user.UserName}</h1>

                <p>Thank you for registering.</p>

                <p>Please confirm your email address by clicking the link below:</p>

                <a href="{confirmationUrl}">
                    Confirm Email
                </a>

                <p>If you did not create this account, ignore this email.</p>
                """;

        await _emailService.SendEmailAsync(
            user.Email,
            emailBody,
            emailSubject);
    }

    #endregion

    #region Public Methods
    public async Task AddAsync(ApplicationUser user, string password)
    {
        var createResult = await _userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            throw new Exception(string.Join(" ", createResult.Errors.Select(e => e.Description)));

        var addToRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
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


    public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        return encodedToken;
    }

    public async Task<string> RegisterAndSendConfirmationEmailAsync(ApplicationUser user, string password, string confirmationUrlTemplate)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await AddAsync(user, password);
            var token = await GenerateEmailConfirmationTokenAsync(user);

            await SendConfirmationEmailAsync(user, token, confirmationUrlTemplate);

            transaction.Commit();
            return token;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task ConfirmEmailAsync(ApplicationUser user, string encodedToken)
    {
        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToken));
        var confirmationResult = await _userManager.ConfirmEmailAsync(user, decodedToken);
        if (!confirmationResult.Succeeded)
            throw new Exception(string.Join(" ", confirmationResult.Errors.Select(e => e.Description)));
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    #endregion
}