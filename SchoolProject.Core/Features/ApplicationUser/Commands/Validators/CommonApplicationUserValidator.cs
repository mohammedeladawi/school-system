using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators;

public class CommonApplicationUserValidator : AbstractValidator<CommonApplicationUserCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    private const string UserNamePattern = "^[a-zA-Z0-9-._@+]+$";
    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public CommonApplicationUserValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        ValidateNameEn();
        ValidateNameAr();
        ValidateUserName();
        ValidateEmail();
    }

    public void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.EmailRequired])

            .Matches(EmailPattern)
            .WithMessage(_localizer[SharedResourceKeys.EmailInvalid]);
    }

    public void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(256)
            .WithMessage(_localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(UserNamePattern)
            .WithMessage(_localizer[SharedResourceKeys.UserNameInvalid]);
    }

    public void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.NameArRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameTooLong]);
    }

    public void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.NameEnRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameTooLong]);
    }

}
