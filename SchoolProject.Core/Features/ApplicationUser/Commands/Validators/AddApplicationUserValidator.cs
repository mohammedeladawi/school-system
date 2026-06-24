using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators;

public class AddStudentValidator : AbstractValidator<AddApplicationUserCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private const string UserNamePattern = "^[a-zA-Z0-9-._@+]+$";
    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public AddStudentValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        ValidateNameEn();
        ValidateNameAr();
        ValidateUserName();
        ValidateEmail();
        ValidatePassword();
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

    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.EmailRequired])

            .Matches(EmailPattern)
            .WithMessage(_localizer[SharedResourceKeys.EmailInvalid]);
    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(256)
            .WithMessage(_localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(UserNamePattern)
            .WithMessage(_localizer[SharedResourceKeys.UserNameInvalid]);
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.NameArRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameTooLong]);
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.NameEnRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameTooLong]);
    }

}
