using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
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

}