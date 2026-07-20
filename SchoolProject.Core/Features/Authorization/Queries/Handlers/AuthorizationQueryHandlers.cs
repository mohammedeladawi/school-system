using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Responses;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers;

public class AuthorizationQueryHandlers :
    ResponseHandler,
    IRequestHandler<GetUserRolesByIdQuery, Response<GetUserRolesByIdQueryResponse>>,
    IRequestHandler<GetUserPermissionClaimsByIdQuery, Response<GetUserPermissionClaimsByIdQueryResponse>>
{
    #region Fields
    private readonly IAuthorizationService _authorizationService;
    private readonly IApplicationUserService _applicationUserService;
    private readonly IApplicationRoleService _applicationRoleService;
    #endregion

    #region Constructors
    public AuthorizationQueryHandlers(
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

    #region Private Methods
    private async Task<List<RoleResponse>> GetUserRolesResponseAsync(List<string> userRoles)
    {
        // Get All Roles
        var allRoles = await _applicationRoleService.GetAllAsync();
        var rolesResponse = new List<RoleResponse>();

        // iterate over them
        foreach (var role in allRoles)
        {
            var roleResponse = new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                HasRole = userRoles.Contains(role.Name)
            };

            rolesResponse.Add(roleResponse);
        }

        return rolesResponse;
    }

    private List<PermissionClaims> GetUserPermissionClaimsResponse(List<Claim> claims)
    {
        // get all
        var permissionClaimsList = new List<PermissionClaims>();
        // foreach => push or not
        foreach (var permissionName in Shared.ClaimStore.PermissionClaims.UserPermissionClaims)
        {
            var claim = new PermissionClaims();
            claim.Name = permissionName;
            claim.Value = claims.Any(c => c.Value == permissionName);

            permissionClaimsList.Add(claim);
        }

        return permissionClaimsList;
    }

    #endregion

    #region Public Methods
    public async Task<Response<GetUserRolesByIdQueryResponse>> Handle(GetUserRolesByIdQuery request, CancellationToken cancellationToken)
    {
        var userRoles = await _authorizationService.GetUserRolesAsync(request.UserId);
        var response = new GetUserRolesByIdQueryResponse
        {
            UserId = request.UserId,
            Roles = await GetUserRolesResponseAsync(userRoles.ToList())
        };

        return Success(response);
    }

    public async Task<Response<GetUserPermissionClaimsByIdQueryResponse>> Handle(GetUserPermissionClaimsByIdQuery request, CancellationToken cancellationToken)
    {
        var userClaims = await _applicationUserService.GetUserClaimsAsync(request.UserId);
        var permissionClaims = userClaims.FindAll(c => c.Type == "Permission");

        var response = new GetUserPermissionClaimsByIdQueryResponse
        {
            UserId = request.UserId,
            UserPermissionClaims = GetUserPermissionClaimsResponse(permissionClaims)
        };

        return Success(response);
    }
    #endregion
}