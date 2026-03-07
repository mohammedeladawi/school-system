using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Queries.Handlers;

public class StudentHandler :
    ResponseHandler,
    IRequestHandler<GetAllStudentsQuery, Response<List<StudentDtoForList>>>
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public StudentHandler(IStudentService studentService, IMapper mapper)
    {
        this._studentService = studentService;
        this._mapper = mapper;
    }

    public async Task<Response<List<StudentDtoForList>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var studentsList = await _studentService.GetAllStudentsAsync();
        var studentsDtoList = _mapper.Map<List<StudentDtoForList>>(studentsList);

        return Created(studentsDtoList);

    }
}