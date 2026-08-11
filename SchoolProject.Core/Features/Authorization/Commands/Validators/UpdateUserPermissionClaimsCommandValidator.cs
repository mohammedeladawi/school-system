// UpdateUserRolesCommandValidator
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators;

public class UpdateUserPermissionClaimsCommandValidator
    : AbstractValidator<UpdateUserPermissionClaimsCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserRepository _ApplicationUserRepositories;
    private readonly IApplicationRoleRepository _applicationRoleService;
    #endregion

    #region Constructors
    public UpdateUserPermissionClaimsCommandValidator(
        IApplicationUserRepository ApplicationUserRepositories,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _localizer = localizer;
        _ApplicationUserRepositories = ApplicationUserRepositories;

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
                    await _ApplicationUserRepositories.DoesExistByIdAsync(userId))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }

    private void ValidateUserPermissionClaims()
    {
        RuleFor(x => x.PermissionClaims)
            .MustAsync(async (permissionClaims, cancellationToken) =>
                permissionClaims.All(pc => Shared.ClaimStore.PermissionClaims.UserPermissionClaims.Contains(pc)))
            .WithMessage(_localizer[SharedResourceKeys.InvalidPermissionClaims]);
    }

    #endregion

}