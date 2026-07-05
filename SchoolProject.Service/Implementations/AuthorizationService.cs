using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations;

public class AuthorizationService : IAuthorizationService
{
    #region Fields
    private readonly IApplicationUserService _applicationUserService;
    #endregion

    #region Constructors
    public AuthorizationService(
        IApplicationUserService applicationUserService
    )
    {
        _applicationUserService = applicationUserService;
    }
    #endregion

    #region Public Methods
    public async Task<IList<string>> GetUserRolesByIdAsync(int userId)
    {
        var user = await _applicationUserService.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception($"User with ID {userId} not found.");
        }

        var userRoles = await _applicationUserService.GetUserRolesByIdAsync(user);
        return userRoles;
    }
    #endregion
}