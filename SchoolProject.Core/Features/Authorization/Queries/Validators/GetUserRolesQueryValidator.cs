using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Interfaces.IdentityServices;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Query.Validators;

public class GetUserRolesQueryValidator : AbstractValidator<GetUserRolesByIdQuery>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _ApplicationUserRepositories;
    #endregion

    #region Constructors
    public GetUserRolesQueryValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager ApplicationUserRepositories)
    {
        _localizer = localizer;
        _ApplicationUserRepositories = ApplicationUserRepositories;

        Include(new UserIdQueryValidator(_localizer, _ApplicationUserRepositories));
    }
    #endregion
}
