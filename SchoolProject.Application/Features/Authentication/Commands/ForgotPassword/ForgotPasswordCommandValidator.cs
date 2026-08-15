using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.ApplicationUser.Commands;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public ForgotPasswordCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateEmail();
    }
    #endregion

    #region Private Methods
    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.Required])

            .EmailAddress()
            .WithMessage(_localizer[SharedResourceKeys.InvalidEmailAddress])

            .MustAsync(async (email, cancellationToken) =>
                await _userManager.DoesEmailExist(email))
            .WithMessage(_localizer[SharedResourceKeys.EmailNotFound]);

    }
    #endregion
}
