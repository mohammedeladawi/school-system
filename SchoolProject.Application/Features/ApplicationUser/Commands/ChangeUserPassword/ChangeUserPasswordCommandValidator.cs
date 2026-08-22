using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Helpers.Validations;
using SchoolProject.Application.Interfaces.Bases;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommandValidator :
    AbstractValidator<ChangeUserPasswordCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public ChangeUserPasswordCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        Include(new BaseChangePasswordCommandValidator<IUserManager, Domain.Entities.Identities.ApplicationUser>(_localizer, _userManager));
    }
    #endregion
}