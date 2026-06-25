using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators;

public class CommonUserCommandValidator : AbstractValidator<CommonUserDto>
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public static readonly string UserNamePattern = "^[a-zA-Z0-9-._@+]+$";
    public static readonly string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public CommonUserCommandValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        ValidateNameEn();
        ValidateNameAr();
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
