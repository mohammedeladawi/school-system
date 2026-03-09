using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Queries.Models;

namespace SchoolProject.Api.Controllers;

public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet("Student/List")]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _mediator.Send(new GetAllStudentsQuery());
        return Ok(students);
    }

    [HttpGet("Student/{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var students = await _mediator.Send(new GetSingleStudentQuery() { Id = id });
        return Ok(students);
    }
}