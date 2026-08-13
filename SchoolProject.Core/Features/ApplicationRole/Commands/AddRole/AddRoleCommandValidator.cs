using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.AddRole;

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructor
    public AddRoleCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IRoleManager roleManager
    )
    {
        _localizer = localizer;
        _roleManager = roleManager;

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
                !await _roleManager.DoesExistByNameAsync(roleName))
            .WithMessage(_localizer[SharedResourceKeys.RoleNameAlreadyExists]);
    }
    #endregion
}