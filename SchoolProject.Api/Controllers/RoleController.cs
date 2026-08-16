using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.ApplicationRole.Commands.AddRole;
using SchoolProject.Application.Features.ApplicationRole.Commands.EditRole;
using SchoolProject.Application.Features.ApplicationRole.Commands.DeleteRole;
using SchoolProject.Application.Features.ApplicationRole.Queries.GetAllRoles;
using SchoolProject.Application.Features.ApplicationRole.Queries.GetRoleById;

namespace SchoolProject.Api.Controllers;

public class RoleController : AppControllerBase
{
    [HttpPost(Router.Role.Add)]
    public async Task<IActionResult> Create(AddRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPut(Router.Role.Update)]
    public async Task<IActionResult> Update(EditRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpDelete(Router.Role.Delete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteRoleByIdCommand(id));
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