using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Emails.Commands.SendEmail;

namespace SchoolProject.Api.Controllers;

public class EmailsController : AppControllerBase
{
    [HttpPost(Router.Emails.Send)]
    public async Task<IActionResult> Send(SendEmailCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }
}
