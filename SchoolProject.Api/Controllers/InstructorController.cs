using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.ApplicationUser.Commands.DeleteInstructorById;
using SchoolProject.Application.Features.ApplicationUser.Commands.EditInstructor;
using SchoolProject.Application.Features.Instructor.Commands.RegisterInstructor;
using SchoolProject.Application.Features.Instructor.Queries.GetInstructorById;
using SchoolProject.Application.Features.Instructor.Queries.GetPaginatedInstructors;

namespace SchoolProject.Api.Controllers;

// Policy based
public class InstructorController : AppControllerBase
{
    [HttpPost(Router.Instructor.Register)]
    public async Task<IActionResult> Register([FromForm] RegisterInstructorCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpGet(Router.Instructor.PaginatedList)]
    public async Task<IActionResult> GetPaginatedList([FromQuery] GetPaginatedInstructorsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet(Router.Instructor.GetById)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await Mediator.Send(new GetInstructorByIdQuery(id));
        return NewResult(result);
    }

    [HttpPut(Router.Instructor.Update)]
    public async Task<IActionResult> Update(EditInstructorCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpDelete(Router.Instructor.Delete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteInstructorByIdCommand(id));
        return NewResult(result);
    }

}