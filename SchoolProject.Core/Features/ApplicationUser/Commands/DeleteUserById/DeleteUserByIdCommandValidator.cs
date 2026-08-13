using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Helpers;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.DeleteUserById;

public class DeleteUserByIdCommandValidator :
    AbstractValidator<DeleteUserByIdCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public DeleteUserByIdCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager)
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateId();
    }
    #endregion

    #region Private Methods
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, CancellationToken) =>
                await _userManager.DoesExistByIdAsync(id))
            .WithMessage(_ => _localizer[SharedResourceKeys.NotFound]);
    }
    #endregion
}