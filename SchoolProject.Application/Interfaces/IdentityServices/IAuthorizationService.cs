using SchoolProject.Domain.Entities.Identities;

namespace SchoolProject.Application.Interfaces.IdentityServices;

public interface IAuthorizationService
{
    public Task<IList<string>> GetUserRolesAsync(int userId);
    public Task UpdateUserRoles(ApplicationUser user, IList<string> roleNames);
    public Task<IList<string>> GetUserPermissionsAsync(int userId);
    public Task UpdateUserPermissionClaims(ApplicationUser user, IList<string> permissionClaims);
}