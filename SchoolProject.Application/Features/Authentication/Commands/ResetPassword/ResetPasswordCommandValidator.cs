using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public ResetPasswordCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateNewPassword();
        ValidateConfirmNewPassword();
        ValidateEncodedUserId();
        ValidateEncodedCode();
    }
    #endregion


    #region Private Methods
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

    }

    private void ValidateConfirmNewPassword()
    {
        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.ConfirmPasswordRequired])

            .Equal(x => x.NewPassword)
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordsDoNotMatch]);
    }

    private void ValidateEncodedUserId()
    {
        RuleFor(x => x.EncodedUserId)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.Required])

            .MustAsync(async (encodedUserId, cancellation) =>
            {
                var userId = Utils.Decode(encodedUserId);
                bool doesExist = await _userManager.DoesExistByIdAsync(Convert.ToInt32(userId));
                return doesExist;
            })
            .WithMessage(_localizer[SharedResourceKeys.InvalidUserId]);
    }

    private void ValidateEncodedCode()
    {
        RuleFor(x => x.EncodedCode)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.Required]);
    }
    #endregion
}
