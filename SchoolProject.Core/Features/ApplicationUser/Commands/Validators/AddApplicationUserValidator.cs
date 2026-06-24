using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators;

public class AddStudentValidator : AbstractValidator<AddApplicationUserCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public AddStudentValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        Include(new CommonApplicationUserValidator(localizer));
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


}
