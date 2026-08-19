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
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public CommonUserCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateEmail();
        ValidateUserName();
        ValidatePhone();
        ValidateNameEn();
        ValidateNameAr();
        ValidateAddress();
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

            .MustAsync(async (email, cancellationToken) =>
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
