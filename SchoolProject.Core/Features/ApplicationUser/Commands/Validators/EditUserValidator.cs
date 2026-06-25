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
            .WithMessage(_localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, CancellationToken) =>
                await _applicationUserService.DoesExistByIdAsync((id)))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }

    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.EmailRequired])

            .Matches(CommonUserCommandValidator.EmailPattern)
            .WithMessage(_localizer[SharedResourceKeys.EmailInvalid])

            .MustAsync(async (user, email, cancellationToken) =>
                !await _applicationUserService.DoesEmailExist(email, user.Id))
            .WithMessage(_localizer[SharedResourceKeys.EmailAlreadyExist]);

    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(256)
            .WithMessage(_localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(CommonUserCommandValidator.UserNamePattern)
            .WithMessage(_localizer[SharedResourceKeys.UserNameInvalid])
  
            .MustAsync(async (user, userName, cancellationToken) =>
                !await _applicationUserService.DoesUserNameExist(userName, user.Id))
            .WithMessage(_localizer[SharedResourceKeys.UserNameAlreadyExist]);

    }
    #endregion

}
