using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Student.Commands.ChangeStudentPassword;
using SchoolProject.Application.Features.Student.Commands.DeleteStudentById;
using SchoolProject.Application.Features.Student.Commands.EditStudent;
using SchoolProject.Application.Features.Student.Commands.RegisterStudent;
using SchoolProject.Application.Features.Student.Queries.GetAllStudents;
using SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;
using SchoolProject.Application.Features.Student.Queries.GetStudentById;

namespace SchoolProject.Api.Controllers;

// Todo: Policy based
public class StudentController : AppControllerBase
{
    [HttpGet(Router.Student.List)]
    public async Task<IActionResult> GetAll()
    {
        var students = await Mediator.Send(new GetAllStudentsQuery());
        return NewResult(students);
    }

    [HttpPost(Router.Student.Register)]
    public async Task<IActionResult> Register([FromForm] RegisterStudentCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpGet(Router.Student.PaginatedList)]
    public async Task<IActionResult> GetPaginatedList([FromQuery] GetPaginatedStudentsQuery query)
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

    [HttpPut(Router.Student.Update)]
    public async Task<IActionResult> Update(EditStudentCommand command)
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

    [HttpPut(Router.Student.ChangePassword)]
    public async Task<IActionResult> ChangePassword(ChangeStudentPasswordCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }
}
