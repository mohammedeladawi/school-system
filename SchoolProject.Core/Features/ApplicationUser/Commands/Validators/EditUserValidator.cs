using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Commands.Validators;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators;

public class EditApplicationUserValidator :
    AbstractValidator<EditUserCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;
    #endregion

    #region Constructors
    public EditApplicationUserValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserService applicationUserService)
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;

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
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, CancellationToken) =>
                await _applicationUserService.DoesExistByIdAsync((id)))
            .WithMessage(_ => _localizer[SharedResourceKeys.NotExist]);
    }

    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailRequired])

            .Matches(CommonUserCommandValidator.EmailPattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailInvalid])

            .MustAsync(async (user, email, cancellationToken) =>
                !await _applicationUserService.DoesEmailExist(email, user.Id))
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailAlreadyInUse]);

    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(256)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(CommonUserCommandValidator.UserNamePattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameInvalid])
  
            .MustAsync(async (user, userName, cancellationToken) =>
                !await _applicationUserService.DoesUserNameExist(userName, user.Id))
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameAlreadyInUse]);

    }
    #endregion

}
