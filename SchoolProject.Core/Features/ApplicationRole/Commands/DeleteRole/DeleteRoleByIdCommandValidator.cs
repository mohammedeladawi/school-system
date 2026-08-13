using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.DeleteRole;

public class DeleteRoleByIdCommandValidator : AbstractValidator<DeleteRoleByIdCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IRoleManager _roleManager;
    #endregion

    #region Constructor
    public DeleteRoleByIdCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IRoleManager roleManager)
    {
        _localizer = localizer;
        _roleManager = roleManager;

        ValidateId();
    }
    #endregion

    #region Validation Rules
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, cancellationToken) =>
            {
                var role = await _roleManager.GetByIdAsync(id);
                return role != null;
            })
            .WithMessage(_localizer[SharedResourceKeys.NotFound]);
    }
    #endregion
}