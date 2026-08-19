using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Features.ApplicationUser.Commands.RegisterUser;

namespace SchoolProject.Application.Features.Authentication.Commands.Register.Admin;

public class RegisterAdminValidator : AbstractValidator<RegisterUserCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public RegisterAdminValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        Include(new CommonRegisterValidator(_localizer, _userManager));
    }
    #endregion

    #region Private Methods
    #endregion

}
