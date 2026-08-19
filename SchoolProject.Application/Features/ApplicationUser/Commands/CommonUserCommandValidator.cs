using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;


namespace SchoolProject.Application.Features.ApplicationUser.Commands;

public class CommonUserCommandValidator : AbstractValidator<CommonUserCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;

    #endregion

    #region Constructors
    public CommonUserCommandValidator(
        IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        ValidatePhone();
        ValidateNameEn();
        ValidateNameAr();
        ValidateAddress();
    }
    #endregion

    #region Private Methods
    private void ValidatePhone()
    {
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20)
            .WithMessage(_ => _localizer[SharedResourceKeys.PhoneTooLong])

            .Matches(RegxPatterns.PhonePattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.PhoneInvalid]);
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameRequired])

            .MaximumLength(100)
            .WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong]);
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameRequired])

            .MaximumLength(100)
            .WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong]);
    }

    private void ValidateAddress()
    {
        RuleFor(x => x.Address)
            .MaximumLength(100)
            .WithMessage(_ => _localizer[SharedResourceKeys.AddressTooLong]);
    }

    #endregion

}
