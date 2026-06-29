using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using Microsoft.AspNetCore.Authorization;

namespace SchoolProject.Api.Controllers;

public class ApplicationUserController : AppControllerBase
{
    [HttpPost(Router.ApplicationUser.Register)]
    public async Task<IActionResult> Add(AddUserCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpGet(Router.ApplicationUser.PaginatedList)]
    public async Task<IActionResult> GetPaginatedList([FromQuery] GetPaginatedUsersQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet(Router.ApplicationUser.GetById)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await Mediator.Send(new GetUserByIdQuery(id));
        return NewResult(result);
    }

    [HttpPut(Router.ApplicationUser.Update)]
    public async Task<IActionResult> Update(EditUserCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete(Router.ApplicationUser.Delete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteCommand(id));
        return NewResult(result);
    }

    [HttpPut(Router.ApplicationUser.ChangePassword)]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

}