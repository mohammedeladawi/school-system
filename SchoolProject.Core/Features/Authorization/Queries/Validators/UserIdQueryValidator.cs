using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Query.Validators;

public class UserIdQueryValidator : AbstractValidator<IUserIdQuery>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserRepository _ApplicationUserRepositories;
    #endregion

    #region Constructors
    public UserIdQueryValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserRepository ApplicationUserRepositories)
    {
        _localizer = localizer;
        _ApplicationUserRepositories = ApplicationUserRepositories;

        ValidateUserId();
    }
    #endregion

    #region Private Methods
    private void ValidateUserId()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.IdRequired])

            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (userId, cancellationToken) =>
                await _ApplicationUserRepositories.DoesExistByIdAsync(userId)
            )
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);


    }
    #endregion
}
