using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Instructor.Commands.AddInstructor;

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
