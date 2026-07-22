// UpdateUserRolesCommandValidator
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators;

public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;
    private readonly IApplicationRoleService _applicationRoleService;
    #endregion

    #region Constructors
    public UpdateUserRolesCommandValidator(
        IApplicationUserService applicationUserService,
        IApplicationRoleService applicationRoleService,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;
        _applicationRoleService = applicationRoleService;

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
                    await _applicationUserService.DoesExistByIdAsync(userId))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }

    private void ValidateRoles()
    {
        RuleFor(x => x.RoleNames)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.RolesRequired])

            .MustAsync(async (roleNames, cancellationToken) =>
                await _applicationRoleService.ValidateRolesExistAsync(roleNames))
            .WithMessage(_localizer[SharedResourceKeys.SomeRolesNotExist]);
    }
    #endregion

}