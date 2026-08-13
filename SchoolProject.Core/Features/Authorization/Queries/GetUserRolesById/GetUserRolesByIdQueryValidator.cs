using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Queries.GetUserRolesById;

public class GetUserRolesByIdQueryValidator : AbstractValidator<GetUserRolesByIdQuery>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public GetUserRolesByIdQueryValidator(
        IUserManager userManager,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _localizer = localizer;
        _userManager = userManager;

        ValidateUserId();
    }
    #endregion

    #region Public Methods
    private void ValidateUserId()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.IdRequired])

            .MustAsync(async (userId, cancellationToken) =>
                    await _userManager.DoesExistByIdAsync(userId))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }
    #endregion
}