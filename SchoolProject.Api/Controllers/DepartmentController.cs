using Microsoft.AspNetCore.Mvc;
using SchoolProject.Shared.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Department.Queries.Models;

namespace SchoolProject.Api.Controllers;

public class DepartmentController : AppControllerBase
{
    [HttpGet(Router.Department.GetById)]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        var department = await Mediator.Send(new GetDepartmentByIdQuery(id));
        return NewResult(department);
    }

    [HttpGet(Router.Department.StudentsCount)]
    public async Task<IActionResult> GetDepartmentStudentsCount()
    {
        var result = await Mediator.Send(new GetDepartmentStudentsCountQuery());
        return NewResult(result);
    }
}