using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Features.ApplicationUser.Commands.RegisterUser;
using SchoolProject.Application.Features.ApplicationUser.Commands;

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

        Include(new CommonUserCommandValidator(_localizer));

        ValidatePassword();
        ValidateEmail();
        ValidateUserName();
    }
    #endregion

    #region Private Methods
    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailRequired])

            .Matches(RegxPatterns.EmailPattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailInvalid])

            .MustAsync(async (command, email, cancellationToken) =>
                !await _userManager.DoesEmailExist(email))
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailAlreadyInUse]);
    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(50)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(RegxPatterns.UserNamePattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameInvalid])

            .MustAsync(async (userName, cancellationToken) =>
                !await _userManager.DoesUserNameExist(userName))
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameAlreadyInUse]);
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
