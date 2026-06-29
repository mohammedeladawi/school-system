using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Validators;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;
    #endregion

    #region Constructors
    public LoginCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserService applicationUserService)
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;

        ValidateUserName();
        ValidatePassword();
    }
    #endregion

    #region Private Methods
    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameRequired])

            .MustAsync(async (userName, cancellationToken) =>
                await _applicationUserService.DoesUserNameExist(userName))
            .WithMessage(_ => _localizer[SharedResourceKeys.InvalidUserNameOrPassword]);
    }

    private void ValidatePassword()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.PasswordRequired]);
    }
    #endregion
}
