using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Features.ApplicationUser.Commands.RegisterUser;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;
using SchoolProject.Application.Helpers.Validations;

namespace SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate.Admin;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public RegisterUserValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        Include(new BaseUserCommandValidator(_localizer));

        ValidatePassword();
        ValidateEmail();
        ValidateUserName();
    }
    #endregion

    #region Private Methods
    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .ValidateEmail(_localizer, _userManager);
    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
        .ValidateUserName(_localizer, _userManager);
    }

    private void ValidatePassword()
    {
        RuleFor(x => x.Password)
            .ValidatePassword(_localizer);

        RuleFor(x => x.ConfirmPassword)
            .ValidateConfirmPassword(x => x.Password, _localizer);
    }

    #endregion

}
