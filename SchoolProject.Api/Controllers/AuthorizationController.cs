using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;

namespace SchoolProject.Api.Controllers;

public class AuthorizationController : AppControllerBase
{
    [HttpPost(Router.Authorization.Create)]
    public async Task<IActionResult> Create(AddRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }
}