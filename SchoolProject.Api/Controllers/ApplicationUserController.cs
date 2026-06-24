using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers;

public class ApplicationUserController : AppControllerBase
{
    [HttpPost(Router.ApplicationUser.Add)]
    public async Task<IActionResult> AddApplicationUser(AddApplicationUserCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpGet(Router.ApplicationUser.PaginatedList)]
    public async Task<IActionResult> GetPaginatedApplicationUsers([FromQuery] GetPaginatedApplicationUsersQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet(Router.ApplicationUser.GetById)]
    public async Task<IActionResult> GetApplicationUserById( int id)
    {
        var result = await Mediator.Send(new GetApplicationUserByIdQuery(id));
        return NewResult(result);
    }

    [HttpPut(Router.ApplicationUser.Edit)]
    public async Task<IActionResult> UpdateApplicationUser( EditApplicationUserCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpDelete(Router.ApplicationUser.Delete)]
    public async Task<IActionResult> DeleteApplicationUserById( int id)
    {
        var result = await Mediator.Send(new DeleteApplicationUserByIdCommand(id));
        return NewResult(result);
    }

}