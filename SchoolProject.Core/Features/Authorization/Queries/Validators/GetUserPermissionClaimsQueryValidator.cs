using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Query.Validators;

public class GetUserPermissionClaimsQueryValidator : AbstractValidator<GetUserPermissionClaimsByIdQuery>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserRepository _ApplicationUserRepositories;
    #endregion

    #region Constructors
    public GetUserPermissionClaimsQueryValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserRepository ApplicationUserRepositories)
    {
        _localizer = localizer;
        _ApplicationUserRepositories = ApplicationUserRepositories;

        Include(new UserIdQueryValidator(_localizer, _ApplicationUserRepositories));
    }
    #endregion
}
