using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Shared.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationRole.Commands.Models;
using SchoolProject.Core.Features.ApplicationRole.Queries.Models;

namespace SchoolProject.Api.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : AppControllerBase
{
    [HttpPost(Router.Role.Create)]
    public async Task<IActionResult> Create(AddRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPut(Router.Role.Edit)]
    public async Task<IActionResult> Edit(EditRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpDelete(Router.Role.Delete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteRoleCommand(id));
        return NewResult(result);
    }

    [HttpGet(Router.Role.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllRolesQuery());
        return NewResult(result);
    }

    [HttpGet(Router.Role.GetById)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await Mediator.Send(new GetRoleByIdQuery(id));
        return NewResult(result);
    }
}