namespace SchoolProject.Core.Features.ApplicationRole.Commands.Validators;


using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IRoleManager _applicationRoleService;
    #endregion

    #region Constructor
    public EditRoleCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IRoleManager applicationRoleService
    )
    {
        _localizer = localizer;
        _applicationRoleService = applicationRoleService;

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
                var role = await _applicationRoleService.GetByIdAsync(id);
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

            .MustAsync(async (command, newName, cancellationToken) =>
                !await _applicationRoleService.DoesExistByNameAsync(newName))
            .WithMessage(_localizer[SharedResourceKeys.RoleNameAlreadyExists]);
    }
    #endregion
}
