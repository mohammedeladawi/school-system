using Microsoft.AspNetCore.Identity;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Infrastructure.Bases;
using Microsoft.Extensions.Logging;

namespace SchoolProject.Infrastructure.IdentityServices;

public class UserManager :
    GenericIdentityUserManagerAsync<ApplicationUser>,
    IUserManager
{
    #region Constructor
    public UserManager(
       UserManager<ApplicationUser> userManager,
       ILogger<UserManager> logger) : base(userManager, logger)
    {
    }
    #endregion

}