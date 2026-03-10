using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers;

public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet(Router.Student.List)]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _mediator.Send(new GetAllStudentsQuery());
        return Ok(students);
    }

    [HttpGet(Router.Student.GetById)]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var students = await _mediator.Send(new GetSingleStudentQuery() { Id = id });
        return Ok(students);
    }

    [HttpPost(Router.Student.Add)]
    public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}