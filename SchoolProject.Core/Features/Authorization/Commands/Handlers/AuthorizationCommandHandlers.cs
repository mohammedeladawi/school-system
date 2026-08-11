using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Interfaces.Identities;
using SchoolProject.Core.Interfaces.Services;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers;

public class AuthorizationCommandHandlers :
    ResponseHandler,
    IRequestHandler<UpdateUserRolesCommand, Response<string>>,
    IRequestHandler<UpdateUserPermissionClaimsCommand, Response<string>>
{
    #region Fields
    private readonly IAuthorizationService _authorizationService;
    private readonly IApplicationUserRepository _ApplicationUserRepositories;
    private readonly IApplicationRoleRepository _applicationRoleService;
    #endregion

    #region Constructors
    public AuthorizationCommandHandlers(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IAuthorizationService authorizationService,
        IApplicationUserRepository ApplicationUserRepositories,
        IApplicationRoleRepository applicationRoleService
        ) : base(localizer, mapper)
    {
        _authorizationService = authorizationService;
        _ApplicationUserRepositories = ApplicationUserRepositories;
        _applicationRoleService = applicationRoleService;
    }

    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await _ApplicationUserRepositories.GetByIdAsync(request.UserId);
        await _authorizationService.UpdateUserRoles(user, request.RoleNames);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }

    public async Task<Response<string>> Handle(UpdateUserPermissionClaimsCommand request, CancellationToken cancellationToken)
    {
        var user = await _ApplicationUserRepositories.GetByIdAsync(request.UserId);
        await _authorizationService.UpdateUserPermissionClaims(user, request.PermissionClaims);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }

    #endregion
}