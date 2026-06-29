using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators;

public class AddStudentValidator : AbstractValidator<AddUserCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;
    #endregion

    #region Constructors
    public AddStudentValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserService applicationUserService)
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;

        Include(new CommonUserCommandValidator(localizer));

        ValidateEmail();
        ValidateUserName();
        ValidatePassword();
    }
    #endregion

    #region Private Methods
    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailRequired])

            .Matches(CommonUserCommandValidator.EmailPattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailInvalid])

            .MustAsync(async (email, cancellationToken) =>
                !await _applicationUserService.DoesEmailExist(email))
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailAlreadyInUse]);

    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(256)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(CommonUserCommandValidator.UserNamePattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameInvalid])

            .MustAsync(async (userName, cancellationToken) =>
                !await _applicationUserService.DoesUserNameExist(userName))
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameAlreadyInUse]);

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

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.ConfirmPasswordRequired])

            .Equal(x => x.Password)
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordsDoNotMatch]);
    }
    #endregion

}
