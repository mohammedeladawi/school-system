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
    IRequestHandler<GetAllStudentsQuery, Response<List<GetAllStudentsQueryResponse>>>,
    IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdQueryResponse>>,
    IRequestHandler<GetPaginatedStudentsQuery, PaginatedResponse<GetPaginatedStudentsQueryResponse>>
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    public StudentQueryHandler(
        IStudentService studentService,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer)
    {
        this._studentService = studentService;
        this._mapper = mapper;
    }

    public async Task<Response<List<GetAllStudentsQueryResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var studentsList = await _studentService.GetAllAsync();
        var studentsDtoList = _mapper.Map<List<GetAllStudentsQueryResponse>>(studentsList);

        return Success(studentsDtoList);
    }

    public async Task<PaginatedResponse<GetPaginatedStudentsQueryResponse>> Handle(GetPaginatedStudentsQuery request, CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
        int totalRecords = await _studentService.GetTotalStudentsCountAsync();
        var students = await _studentService.GetPaginatedStudentsAsync(pageNumber, pageSize, request.SearchTerm, request.OrderBy);
        var studentsDto = _mapper.Map<List<GetPaginatedStudentsQueryResponse>>(students);

        var paginatedResponse = new PaginatedResponse<GetPaginatedStudentsQueryResponse>(studentsDto, pageNumber, pageSize, totalRecords);
        return paginatedResponse;
    }

    public async Task<Response<GetStudentByIdQueryResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetByIdAsync(request.Id);
        if (student is null) return NotFound<GetStudentByIdQueryResponse>(_localizer[SharedResourceKeys.NotFound]);

        var studentDto = _mapper.Map<GetStudentByIdQueryResponse>(student);
        return Success(studentDto);
    }
}