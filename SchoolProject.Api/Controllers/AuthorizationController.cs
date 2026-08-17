using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Authorization.Commands.UpdateUserRoles;
using SchoolProject.Application.Features.Authorization.Commands.UpdateUserPermissionClaims;
using SchoolProject.Application.Features.Authorization.Queries.GetUserRolesById;
using SchoolProject.Application.Features.Authorization.Queries.GetUserPermissionClaimsById;

namespace SchoolProject.Api.Controllers;

// Todo: initially, Super Admin
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