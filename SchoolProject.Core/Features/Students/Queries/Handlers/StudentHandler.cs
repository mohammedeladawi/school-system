using MediatR;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Queries.Handlers;

public class StudentHandler : IRequestHandler<GetAllStudentsQuery, List<Student>>
{
    private readonly IStudentService _studentService;

    public StudentHandler(IStudentService studentService)
    {
        this._studentService = studentService;
    }

    public async Task<List<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _studentService.GetAllStudentsAsync();
    }
}