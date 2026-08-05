using Microsoft.AspNetCore.Mvc;
using SchoolProject.Shared.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.Features.Instructor.Commands.Models;

namespace SchoolProject.Api.Controllers;

public class InstructorController : AppControllerBase
{
    [HttpPost(Router.Instructor.Add)]
    public async Task<IActionResult> Send([FromForm] AddInstructorCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }
}
