using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Instructor.Commands.RegisterInstructor;

namespace SchoolProject.Api.Controllers;

// Policy based
public class InstructorController : AppControllerBase
{
    // [HttpPost(Router.Instructor.Add)]
    // public async Task<IActionResult> Send([FromForm] AddInstructorCommand command)
    // {
    //     var result = await Mediator.Send(command);
    //     return NewResult(result);
    // }

    [HttpPost(Router.Instructor.Register)]
    public async Task<IActionResult> Register([FromForm] RegisterInstructorCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }
}
