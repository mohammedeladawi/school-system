using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Query.Validators;

public class GetUserRolesQueryValidator : AbstractValidator<GetUserRolesByIdQuery>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IApplicationUserService _applicationUserService;
    #endregion

    #region Constructors
    public GetUserRolesQueryValidator(
        IStringLocalizer<SharedResource> localizer,
        IApplicationUserService applicationUserService)
    {
        _localizer = localizer;
        _applicationUserService = applicationUserService;

        Include(new UserIdQueryValidator(_localizer, _applicationUserService));
    }
    #endregion
}
