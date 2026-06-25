using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Core.Features.Student.Queries.Models;


namespace SchoolProject.Api.Controllers;

public class StudentController : AppControllerBase
{
    [HttpGet(Router.Student.List)]
    public async Task<IActionResult> GetAll()
    {
        var students = await Mediator.Send(new GetAllStudentsQuery());
        return NewResult(students);
    }

    [HttpGet(Router.Student.PaginatedList)]
    public async Task<IActionResult> GetPaginated([FromQuery] GetPaginatedStudentsQuery query)
    {
        var paginatedStudents = await Mediator.Send(query);
        return Ok(paginatedStudents);
    }

    [HttpGet(Router.Student.GetById)]
    public async Task<IActionResult> GetById(int id)
    {
        var students = await Mediator.Send(new GetStudentByIdQuery(id));
        return NewResult(students);
    }

    [HttpPost(Router.Student.Add)]
    public async Task<IActionResult> Add([FromBody] AddStudentCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPut(Router.Student.Update)]
    public async Task<IActionResult> Update([FromBody] EditStudentCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpDelete(Router.Student.Delete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteStudentByIdCommand(id));
        return NewResult(result);
    }
}
