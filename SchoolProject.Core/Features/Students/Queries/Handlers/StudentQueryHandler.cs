using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Queries.Handlers;

public class StudentQueryHandler :
    ResponseHandler,
    IRequestHandler<GetAllStudentsQuery, Response<List<StudentDtoForList>>>,
    IRequestHandler<GetSingleStudentQuery, Response<SingleStudentDto>>,
    IRequestHandler<GetPaginatedStudentsQuery, PaginatedResponse<PaginatedStudentsDto>>
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    public StudentQueryHandler(IStudentService studentService, IMapper mapper,
        IStringLocalizer<SharedResource> localizer)
        : base(localizer)
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

    public async Task<PaginatedResponse<PaginatedStudentsDto>> Handle(GetPaginatedStudentsQuery request, CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
        int totalRecords = await _studentService.GetTotalStudentsCountAsync();
        var students = await _studentService.GetPaginatedStudentsAsync(pageNumber, pageSize, request.SearchTerm, request.OrderBy);
        var studentsDto = _mapper.Map<List<PaginatedStudentsDto>>(students);

        var paginatedResponse = new PaginatedResponse<PaginatedStudentsDto>(studentsDto, pageNumber, pageSize, totalRecords);
        return paginatedResponse;
    }

    public async Task<Response<SingleStudentDto>> Handle(GetSingleStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        if (student is null) return NotFound<SingleStudentDto>(_localizer["NotFound"]);

        var studentDto = _mapper.Map<SingleStudentDto>(student);
        return Success(studentDto);
    }
}