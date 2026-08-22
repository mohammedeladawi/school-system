using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public LoginCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateUserName();
        ValidatePassword();
    }
    #endregion

    #region Private Methods
    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameRequired])

            .MustAsync(async (userName, cancellationToken) =>
                await _userManager.DoesUserNameExist(userName))
            .WithMessage(_ => _localizer[SharedResourceKeys.InvalidUserNameOrPassword]);
    }

    private void ValidatePassword()
    {
        RuleFor(x => x.Password)
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

    #endregion
}
