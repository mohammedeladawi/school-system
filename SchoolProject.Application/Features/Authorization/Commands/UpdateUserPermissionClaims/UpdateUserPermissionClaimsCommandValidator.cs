using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Authorization.Commands.UpdateUserPermissionClaims;

public class UpdateUserPermissionClaimsCommandValidator
    : AbstractValidator<UpdateUserPermissionClaimsCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public UpdateUserPermissionClaimsCommandValidator(
        IUserManager userManager,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateUserId();
        ValidateUserPermissionClaims();
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

    private void ValidateUserPermissionClaims()
    {
        RuleFor(x => x.PermissionClaims)
            .MustAsync(async (permissionClaims, cancellationToken) =>
                permissionClaims.All(pc => Domain.ClaimStore.PermissionClaims.UserPermissionClaims.Contains(pc)))
            .WithMessage(_localizer[SharedResourceKeys.InvalidPermissionClaims]);
    }

    #endregion
}