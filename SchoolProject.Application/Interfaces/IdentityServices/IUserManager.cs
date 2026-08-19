using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Domain.Entities.Identities;

namespace SchoolProject.Application.Interfaces.IdentityServices;

public interface IUserManager
{
    Task<List<ApplicationUser>> GetPaginatedListAsync(int pageNumber, int pageSize);
    Task AddAsync<TUser>(TUser user, string password, string role) where TUser : ApplicationUser;
    Task<ApplicationUser?> GetByIdAsync(int id);
    Task UpdateAsync(ApplicationUser user);
    Task DeleteAsync(ApplicationUser user);
    Task<bool> DoesExistByIdAsync(int id);
    Task<int> GetTotalCountAsync();
    Task ChangePasswordAsync(ApplicationUser user, string newPassword);
    Task ChangePasswordAsync(int id, string currentPassword, string newPassword);
    Task<bool> DoesEmailExist(string email, int? excludeUserId = null);
    Task<bool> DoesUserNameExist(string userName, int? excludeUserId = null);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetByUserNameAndPasswordAsync(string userName, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
    Task ConfirmEmailAsync(ApplicationUser user, string token);
}