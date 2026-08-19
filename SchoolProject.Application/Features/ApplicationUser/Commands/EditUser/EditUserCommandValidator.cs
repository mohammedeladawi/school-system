using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditUser;

public class EditUserCommandValidator :
    AbstractValidator<EditUserCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public EditUserCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        Include(new CommonUserCommandValidator(localizer));

        ValidateId();
        ValidateEmail();
        ValidateUserName();
    }
    #endregion

    #region Private Methods
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .ValidateUserId(_localizer, _userManager.DoesExistByIdAsync);
    }

    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailRequired])

            .Matches(RegxPatterns.EmailPattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailInvalid])

            .MustAsync(async (command, email, cancellationToken) =>
                !await _userManager.DoesEmailExist(email, command.Id))
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

            .MustAsync(async (command, userName, cancellationToken) =>
                !await _userManager.DoesUserNameExist(userName, command.Id))
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameAlreadyInUse]);
    }


    #endregion
}