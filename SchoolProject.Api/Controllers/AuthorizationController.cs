using Microsoft.AspNetCore.Mvc;
using SchoolProject.Shared.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authorization.Commands.UpdateUserRoles;
using SchoolProject.Core.Features.Authorization.Commands.UpdateUserPermissionClaims;
using SchoolProject.Core.Features.Authorization.Queries.GetUserRolesById;
using SchoolProject.Core.Features.Authorization.Queries.GetUserPermissionClaimsById;

namespace SchoolProject.Api.Controllers;

public class AuthorizationController : AppControllerBase
{
    [HttpGet(Router.Authorization.GetUserRolesById)]
    public async Task<IActionResult> GetUserRolesById(int userId)
    {
        var result = await Mediator.Send(new GetUserRolesByIdQuery(userId));
        return NewResult(result);
    }

    [HttpPut(Router.Authorization.UpdateUserRoles)]
    public async Task<IActionResult> UpdateUserRoles(UpdateUserRolesCommand request)
    {
        var result = await Mediator.Send(request);
        return NewResult(result);
    }

    [HttpGet(Router.Authorization.GetUserPermissionClaims)]
    public async Task<IActionResult> GetUserPermissionClaims(int userId)
    {
        var result = await Mediator.Send(new GetUserPermissionClaimsByIdQuery(userId));
        return NewResult(result);
    }

    [HttpPut(Router.Authorization.UpdateUserPermissionClaims)]
    public async Task<IActionResult> UpdateUserPermissionClaims(UpdateUserPermissionClaimsCommand request)
    {
        var result = await Mediator.Send(request);
        return NewResult(result);
    }




}