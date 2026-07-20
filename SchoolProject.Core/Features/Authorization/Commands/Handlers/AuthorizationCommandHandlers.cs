using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers;

public class AuthorizationCommandHandlers :
    ResponseHandler,
    IRequestHandler<UpdateUserRolesCommand, Response<string>>,
    IRequestHandler<UpdateUserPermissionClaimsCommand, Response<string>>
{
    #region Fields
    private readonly IAuthorizationService _authorizationService;
    private readonly IApplicationUserService _applicationUserService;
    private readonly IApplicationRoleService _applicationRoleService;
    #endregion

    #region Constructors
    public AuthorizationCommandHandlers(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IAuthorizationService authorizationService,
        IApplicationUserService applicationUserService,
        IApplicationRoleService applicationRoleService
        ) : base(localizer, mapper)
    {
        _authorizationService = authorizationService;
        _applicationUserService = applicationUserService;
        _applicationRoleService = applicationRoleService;
    }

    #endregion

    #region Public Methods
    public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await _applicationUserService.GetByIdAsync(request.UserId);
        await _authorizationService.UpdateUserRoles(user, request.RoleNames);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }

    public async Task<Response<string>> Handle(UpdateUserPermissionClaimsCommand request, CancellationToken cancellationToken)
    {
        var user = await _applicationUserService.GetByIdAsync(request.UserId);
        await _authorizationService.UpdateUserPermissionClaims(user, request.PermissionClaims);
        return Success<string>(_localizer[SharedResourceKeys.UpdatedSuccessfully]);
    }

    #endregion
}