using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Queries.Handlers;

public class StudentQueryHandler :
    ResponseHandler,
    IRequestHandler<GetAllStudentsQuery, Response<List<StudentDtoForList>>>,
    IRequestHandler<GetSingleStudentQuery, Response<SingleStudentDto>>
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public StudentQueryHandler(IStudentService studentService, IMapper mapper)
    {
        this._studentService = studentService;
        this._mapper = mapper;
    }

    public async Task<Response<List<StudentDtoForList>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var studentsList = await _studentService.GetAllStudentsAsync();
        var studentsDtoList = _mapper.Map<List<StudentDtoForList>>(studentsList);

        return Success(studentsDtoList);
    }

    public async Task<Response<SingleStudentDto>> Handle(GetSingleStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        if (student is null) return NotFound<SingleStudentDto>();

        var studentDto = _mapper.Map<SingleStudentDto>(student);
        return Success(studentDto);
    }
}