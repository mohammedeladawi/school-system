namespace SchoolProject.Core.Features.ApplicationRole.Commands.Validators;


using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IRoleManager _applicationRoleService;
    #endregion

    #region Constructor
    public AddRoleCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IRoleManager applicationRoleService
    )
    {
        _localizer = localizer;
        _applicationRoleService = applicationRoleService;

        ValidateRoleName();
    }
    #endregion

    #region Validation Rules
    private void ValidateRoleName()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.RoleNameRequired])

            .MaximumLength(50)
            .WithMessage(_localizer[SharedResourceKeys.RoleNameIsTooLong])

            .MustAsync(async (roleName, cancellationToken) =>
                !await _applicationRoleService.DoesExistByNameAsync(roleName))
            .WithMessage(_localizer[SharedResourceKeys.RoleNameAlreadyExists]);
    }
    #endregion
}