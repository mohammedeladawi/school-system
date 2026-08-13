using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.VerifyResetCode;

public class VerifyResetCodeCommandValidator : AbstractValidator<VerifyResetCodeCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public VerifyResetCodeCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateEmail();
        ValidateCode();
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
    private void ValidateCode()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.Required])

            .Length(6)
            .WithMessage(_localizer[SharedResourceKeys.InvalidOTP]);
    }
    #endregion
}
