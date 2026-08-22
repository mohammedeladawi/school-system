using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Helpers.Validations;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;

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

        Include(new BaseUserCommandValidator(localizer));

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
            .ValidateEmail(_localizer, _userManager, x => x.Id);
    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .ValidateUserName(_localizer, _userManager, x => x.Id);
    }


    #endregion
}