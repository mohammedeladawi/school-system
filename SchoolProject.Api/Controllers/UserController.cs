using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Application.Features.ApplicationUser.Commands.EditUser;
using SchoolProject.Application.Features.ApplicationUser.Commands.DeleteUserById;
using SchoolProject.Application.Features.ApplicationUser.Commands.ChangePassword;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using SchoolProject.Application.Features.ApplicationUser.Commands.RegisterUser;

namespace SchoolProject.Api.Controllers;

// Todo: All for Super Admin, unless get-by-id, change-password, and update are policy based;
public class ApplicationUserController : AppControllerBase
{
    [HttpPost(Router.ApplicationUser.Register)]
    public async Task<IActionResult> Register([FromForm] RegisterUserCommand command)
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

    [HttpDelete(Router.ApplicationUser.Delete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteUserByIdCommand(id));
        return NewResult(result);
    }

    [Authorize(Policy = "User.ChangePassword")]
    [HttpPut(Router.ApplicationUser.ChangePassword)]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

}