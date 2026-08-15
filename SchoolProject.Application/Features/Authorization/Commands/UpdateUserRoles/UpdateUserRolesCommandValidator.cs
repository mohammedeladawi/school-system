using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Authorization.Commands.UpdateUserRoles;

public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructors
    public UpdateUserRolesCommandValidator(
        IUserManager userManager,
        IRoleManager roleManager,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _localizer = localizer;
        _userManager = userManager;
        _roleManager = roleManager;

        ValidateUserId();
        ValidateRoles();
    }
    #endregion

    #region Public Methods
    private void ValidateUserId()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.IdRequired])

            .MustAsync(async (userId, cancellationToken) =>
                    await _userManager.DoesExistByIdAsync(userId))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }

    private void ValidateRoles()
    {
        RuleFor(x => x.RoleNames)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.RolesRequired])

        .MustAsync(async (roleNames, cancellationToken) =>
            await _roleManager.ValidateRolesExistAsync(roleNames.ToArray()))
        .WithMessage(_localizer[SharedResourceKeys.SomeRolesNotExist]);
    }
    #endregion
}