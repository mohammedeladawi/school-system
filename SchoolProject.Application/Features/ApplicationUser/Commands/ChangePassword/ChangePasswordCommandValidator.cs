using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public ChangePasswordCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateId();
        ValidateCurrentPassword();
        ValidateNewPassword();
    }
    #endregion

    #region Private Methods
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .ValidateUserId(_localizer, _userManager.DoesExistByIdAsync);
    }

    private void ValidateCurrentPassword()
    {
        RuleFor(x => x.CurrentPassword)
            .ValidatePassword(_localizer);
    }

    private void ValidateNewPassword()
    {
        RuleFor(x => x.NewPassword)
            .ValidatePassword(_localizer);

        RuleFor(x => x.ConfirmNewPassword)
            .ValidateConfirmPassword(x => x.NewPassword, _localizer);
    }

    #endregion
}