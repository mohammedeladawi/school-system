using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;

namespace SchoolProject.Api.Controllers;

[Authorize(Roles = "Admin")]
public class AuthorizationController : AppControllerBase
{
    [HttpPost(Router.Authorization.CreateRole)]
    public async Task<IActionResult> Create(AddRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPut(Router.Authorization.EditRole)]
    public async Task<IActionResult> Edit(EditRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpDelete(Router.Authorization.DeleteRole)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteRoleCommand(id));
        return NewResult(result);
    }
}