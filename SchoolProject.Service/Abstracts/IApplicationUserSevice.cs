using System.Security.Claims;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Service.Abstracts;

public interface IApplicationUserService
{
    protected Task AddAsync(ApplicationUser user, string password);
    protected Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
    public Task<string> RegisterAndSendConfirmationEmailAsync(ApplicationUser user, string password, string confirmationUrlTemplate);
    public Task ConfirmEmailAsync(ApplicationUser user, string token);
    public Task<ApplicationUser?> GetByIdAsync(int id);
    public Task UpdateAsync(ApplicationUser user);
    public Task DeleteAsync(ApplicationUser user);
    public Task<bool> DoesExistByIdAsync(int id);
    public Task<int> GetTotalCountAsync();
    public Task ChangePasswordAsync(int id, string currentPassword, string newPassword);
    public Task<bool> DoesEmailExist(string email, int? excludeUserId = null);
    public Task<List<ApplicationUser>> GetPaginatedListAsync(int pageNumber, int pageSize);
    public Task<bool> DoesUserNameExist(string userName, int? excludeUserId = null);
    public Task<ApplicationUser?> GetByUserNameAndPasswordAsync(string userName, string password);
    public Task<List<string>> GetUserRolesAsync(ApplicationUser user);
    public Task<ApplicationUser?> GetByEmailAsync(string email);

}