using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserRepository _ApplicationUserRepositories;

    public ResetPasswordCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserRepository ApplicationUserRepositories)
    {
        _localizer = localizer;
        _ApplicationUserRepositories = ApplicationUserRepositories;

        ValidateNewPassword();
        ValidateEncodedUserId();
        ValidateEncodedCode();
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

    private void ValidateEncodedUserId()
    {
        RuleFor(x => x.EncodedUserId)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.Required]);
    }

    private void ValidateEncodedCode()
    {
        RuleFor(x => x.EncodedCode)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.Required]);
    }
}
