using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;
    #endregion

    #region Constructors
    public ChangePasswordValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserService applicationUserService)
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;

        ValidateId();
        ValidateCurrentPassword();
        ValidateNewPassword();
    }
    #endregion

    #region Private Methods
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, CancellationToken) =>
                await _applicationUserService.DoesExistByIdAsync((id)))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }

    private void ValidateCurrentPassword()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.PasswordRequired]);
    }

    private void ValidateNewPassword()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.PasswordRequired])

            .MinimumLength(6)
            .WithMessage(_localizer[SharedResourceKeys.PasswordMinimumLength])

            .Matches("[A-Z]")
            .WithMessage(_localizer[SharedResourceKeys.PasswordRequireUppercase])

            .Matches("[a-z]")
            .WithMessage(_localizer[SharedResourceKeys.PasswordRequireLowercase])

            .Matches("\\d")
            .WithMessage(_localizer[SharedResourceKeys.PasswordRequireDigit])

            .Matches("[^\\w\\s]")
            .WithMessage(_localizer[SharedResourceKeys.PasswordRequireNonAlphanumeric]);

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.ConfirmPasswordRequired])

            .Equal(x => x.NewPassword)
            .WithMessage(_localizer[SharedResourceKeys.PasswordsDoNotMatch]);
    }
    #endregion

}
