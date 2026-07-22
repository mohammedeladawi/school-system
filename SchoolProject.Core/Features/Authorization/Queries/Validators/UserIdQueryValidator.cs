using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Query.Validators;

public class UserIdQueryValidator : AbstractValidator<IUserIdQuery>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;
    #endregion

    #region Constructors
    public UserIdQueryValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserService applicationUserService)
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;

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
                await _applicationUserService.DoesExistByIdAsync(userId)
            )
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);


    }
    #endregion
}
