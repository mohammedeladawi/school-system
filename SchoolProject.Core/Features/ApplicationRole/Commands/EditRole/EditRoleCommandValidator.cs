using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.EditRole;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructor
    public EditRoleCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IRoleManager roleManager
    )
    {
        _localizer = localizer;
        _roleManager = roleManager;

        ValidateId();
        ValidateNewName();
    }
    #endregion

    #region Validation Rules
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, cancellationToken) =>
            {
                var role = await _roleManager.GetByIdAsync(id);
                return role != null;
            })
            .WithMessage(_localizer[SharedResourceKeys.NotFound]);
    }

    private void ValidateNewName()
    {
        RuleFor(x => x.NewName)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.RoleNameRequired])

            .MaximumLength(50)
            .WithMessage(_localizer[SharedResourceKeys.RoleNameIsTooLong])

            .MustAsync(async (newName, cancellationToken) =>
                 !await _roleManager.DoesExistByNameAsync(newName))
            .WithMessage(_localizer[SharedResourceKeys.RoleNameAlreadyExists]);
    }
    #endregion
}