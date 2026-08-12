using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Validators;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _ApplicationUserRepositories;
    #endregion

    #region Constructors
    public LoginCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager ApplicationUserRepositories)
    {
        _localizer = localizer;
        _ApplicationUserRepositories = ApplicationUserRepositories;

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
                await _ApplicationUserRepositories.DoesUserNameExist(userName))
            .WithMessage(_ => _localizer[SharedResourceKeys.InvalidUserNameOrPassword]);
    }

    private void ValidatePassword()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequired]);
    }
    #endregion
}
