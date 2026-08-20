using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Helpers.Validations;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.DeleteUserById;

public class DeleteUserByIdCommandValidator :
    AbstractValidator<DeleteUserByIdCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public DeleteUserByIdCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateId();
    }
    #endregion

    #region Private Methods
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .ValidateUserId(_localizer, _userManager.DoesExistByIdAsync);
    }
    #endregion
}