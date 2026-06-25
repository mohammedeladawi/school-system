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
            .WithMessage(_localizer[SharedResourceKeys.EmailRequired])

            .Matches(CommonUserCommandValidator.EmailPattern)
            .WithMessage(_localizer[SharedResourceKeys.EmailInvalid])

            .MustAsync(async (email, cancellationToken) =>
                !await _applicationUserService.DoesEmailExist(email))
            .WithMessage(_localizer[SharedResourceKeys.EmailAlreadyExist]);

    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(256)
            .WithMessage(_localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(CommonUserCommandValidator.UserNamePattern)
            .WithMessage(_localizer[SharedResourceKeys.UserNameInvalid])

            .MustAsync(async (userName, cancellationToken) =>
                !await _applicationUserService.DoesUserNameExist(userName))
            .WithMessage(_localizer[SharedResourceKeys.UserNameAlreadyExist]);

    }

    private void ValidatePassword()
    {
        RuleFor(x => x.Password)
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

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.ConfirmPasswordRequired])

            .Equal(x => x.Password)
            .WithMessage(_localizer[SharedResourceKeys.PasswordsDoNotMatch]);
    }
    #endregion

}
