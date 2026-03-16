using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers;

public class StudentController : AppControllerBase
{
    [HttpGet(Router.Student.List)]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await Mediator.Send(new GetAllStudentsQuery());
        return NewResult(students);
    }

    [HttpGet(Router.Student.GetById)]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var students = await Mediator.Send(new GetSingleStudentQuery() { Id = id });
        return NewResult(students);
    }

    [HttpPost(Router.Student.Add)]
    public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPut(Router.Student.Edit)]
    public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpDelete(Router.Student.Delete)]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var result = await Mediator.Send(new DeleteStudentCommand(id));
        return NewResult(result);
    }
}