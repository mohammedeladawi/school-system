using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Api.Controllers;

public class AuthenticationController : AppControllerBase
{
    [HttpPost(Router.Authentication.Login)]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }
}
