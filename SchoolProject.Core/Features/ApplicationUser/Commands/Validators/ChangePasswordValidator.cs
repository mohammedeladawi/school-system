using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserRepository _ApplicationUserRepositories;
    #endregion

    #region Constructors
    public ChangePasswordValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserRepository ApplicationUserRepositories)
    {
        _localizer = localizer;
        _ApplicationUserRepositories = ApplicationUserRepositories;

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
            .WithMessage(_ => _localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, CancellationToken) =>
                await _ApplicationUserRepositories.DoesExistByIdAsync((id)))
            .WithMessage(_ => _localizer[SharedResourceKeys.NotExist]);
    }

    private void ValidateCurrentPassword()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequired]);
    }

    private void ValidateNewPassword()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequired])

            .MinimumLength(6)
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordMinimumLength])

            .Matches("[A-Z]")
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequireUppercase])

            .Matches("[a-z]")
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequireLowercase])

            .Matches("\\d")
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequireDigit])

            .Matches("[^\\w\\s]")
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequireNonAlphanumeric]);

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.ConfirmPasswordRequired])

            .Equal(x => x.NewPassword)
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordsDoNotMatch]);
    }
    #endregion

}
