using System.Linq.Expressions;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Domain.Entities.Identities;

namespace SchoolProject.Application.Interfaces.Bases;

public interface IGenericIdentityUserManagerAsync<TUser> where TUser : ApplicationUser
{
    public Task<List<TUser>> GetAllAsync(
        Expression<Func<TUser, object>>[]? includes = null,
        Expression<Func<TUser, bool>>? filter = null,
        bool asNoTracking = true
    );

    Task<List<TUser>> GetPaginatedListAsync(
    int pageNumber = 1,
    int pageSize = 10,
     Expression<Func<TUser, object>>[]? includes = null,
     Expression<Func<TUser, bool>>? filter = null,
     bool asNoTracking = true);

    Task AddAsync(TUser user, string password, string role);
    Task<TUser?> GetByIdAsync(
        int id,
        Expression<Func<TUser, object>>[]? includes = null);
    Task UpdateAsync(TUser user);
    Task DeleteAsync(TUser user);
    Task<bool> DoesExistByIdAsync(int id);
    Task<int> GetTotalCountAsync();
    Task<bool> CheckPasswordAsync(int id, string password);
    Task ChangePasswordAsync(TUser user, string newPassword);
    Task ChangePasswordAsync(int id, string currentPassword, string newPassword);
    Task<bool> DoesEmailExist(string email, int? excludeUserId = null);
    Task<bool> DoesUserNameExist(string userName, int? excludeUserId = null);
    Task<TUser?> GetByEmailAsync(string email);
    Task<TUser?> GetByUserNameAndPasswordAsync(string userName, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(TUser user);
    Task ConfirmEmailAsync(TUser user, string token);
}