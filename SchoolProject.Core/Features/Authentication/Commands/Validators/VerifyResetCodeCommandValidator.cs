using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Validators;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators;

public class VerifyResetCodeCommandValidator : AbstractValidator<VerifyResetCodeCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    #endregion

    #region Constructors
    public VerifyResetCodeCommandValidator(
        IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

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
            .WithMessage(_localizer[SharedResourceKeys.InvalidEmailAddress]);

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
