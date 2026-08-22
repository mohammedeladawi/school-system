using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;
using SchoolProject.Application.Helpers.Validations;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Base.Users.Commands.Validators;

public class BaseChangePasswordCommandValidator<TManager, TUser> :
    AbstractValidator<BaseChangePasswordCommand>
    where TManager : IGenericIdentityUserManagerAsync<TUser>
    where TUser : Domain.Entities.Identities.ApplicationUser
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly TManager _userManager;
    #endregion

    #region Constructors
    public BaseChangePasswordCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        TManager userManager)
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