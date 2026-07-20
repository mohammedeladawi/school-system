using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;

namespace SchoolProject.Api.Controllers;

public class AuthorizationController : AppControllerBase
{
    [HttpGet(Router.Authorization.GetUserRolesById)]
    public async Task<IActionResult> GetUserRolesById(int userId)
    {
        var result = await Mediator.Send(new GetUserRolesByIdQuery(userId));
        return NewResult(result);
    }

    [HttpPut(Router.Authorization.GetUserPermissionClaims)]
    public async Task<IActionResult> GetUserPermissionClaims(GetUserPermissionClaimsByIdQuery request)
    {
        var result = await Mediator.Send(new GetUserPermissionClaimsByIdQuery(request.UserId));
        return NewResult(result);
    }
}