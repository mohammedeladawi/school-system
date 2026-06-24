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
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public ApplicationUserService(
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<SharedResource> localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
    }

    public async Task AddApplicationUserAsync(ApplicationUser user, string password)
    {
        ArgumentNullException.ThrowIfNull(user.UserName);
        ArgumentNullException.ThrowIfNull(user.Email);

        // Check if Username Exists, if yes throw conflict error
        if (await _userManager.FindByNameAsync(user.UserName) is not null)
            throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

        // Check if Email Exists, if yes throw conflict error
        if (await _userManager.FindByEmailAsync(user.Email) is not null)
            throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

        // Add role
        // var addRoleResult = await _userManager.AddToRoleAsync(user, "USER");
        // if (!addRoleResult.Succeeded)
        //     throw new Exception(string.Join(" ", addRoleResult.Errors.Select(e => e.Description)));

        // Create 
        var createResult = await _userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            throw new Exception(string.Join(" ", createResult.Errors.Select(e => e.Description)));
    }

    public async Task<List<ApplicationUser>> GetAllApplicationUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<List<ApplicationUser>> GetPaginatedApplicationUsersAsync(int pageNumber, int pageSize)
    {
        var paginatedApplicationUsers = await _userManager.Users.OrderBy(au => au.Id)
                                      .Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();
        return paginatedApplicationUsers;
    }

    public async Task<int> GetTotalApplicationUsersCountAsync()
    {
        int usersCount = await _userManager.Users.CountAsync();
        return usersCount;
    }

    public async Task<ApplicationUser?> GetApplicationUserByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return user;
    }
}