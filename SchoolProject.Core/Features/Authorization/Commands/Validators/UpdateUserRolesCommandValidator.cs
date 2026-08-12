// UpdateUserRolesCommandValidator
using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators;

public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _ApplicationUserRepositories;
    private readonly IRoleManager _applicationRoleService;
    #endregion

    #region Constructors
    public UpdateUserRolesCommandValidator(
        IUserManager ApplicationUserRepositories,
        IRoleManager applicationRoleService,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _localizer = localizer;
        _ApplicationUserRepositories = ApplicationUserRepositories;
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
                    await _ApplicationUserRepositories.DoesExistByIdAsync(userId))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }

    private void ValidateRoles()
    {
        RuleFor(x => x.RoleNames)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.RolesRequired]);

        // Todo: Add this logic into the handler
        // .MustAsync(async (roleNames, cancellationToken) =>
        //     await _applicationRoleService.ValidateRolesExistAsync(roleNames))
        // .WithMessage(_localizer[SharedResourceKeys.SomeRolesNotExist]);
    }
    #endregion

}