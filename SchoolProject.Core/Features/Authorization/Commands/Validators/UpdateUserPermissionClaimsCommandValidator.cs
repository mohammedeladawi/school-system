// UpdateUserRolesCommandValidator
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators;

public class UpdateUserPermissionClaimsCommandValidator
    : AbstractValidator<UpdateUserPermissionClaimsCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;
    private readonly IApplicationRoleService _applicationRoleService;
    #endregion

    #region Constructors
    public UpdateUserPermissionClaimsCommandValidator(
        IApplicationUserService applicationUserService,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;

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
                    await _applicationUserService.DoesExistByIdAsync(userId))
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