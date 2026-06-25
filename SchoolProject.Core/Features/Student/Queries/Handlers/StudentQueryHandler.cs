using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Queries.Models;
using SchoolProject.Core.Features.Student.Queries.Responses;
using SchoolProject.Shared.Resources;
using SchoolProject.Core.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Student.Queries.Handlers;

public class StudentQueryHandler :
    ResponseHandler,
    IRequestHandler<GetAllStudentsQuery, Response<List<GetAllStudentsQueryResponse>>>,
    IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdQueryResponse>>,
    IRequestHandler<GetPaginatedStudentsQuery, PaginatedResponse<GetPaginatedStudentsQueryResponse>>
{
    #region Private Fields
    private readonly IStudentService _studentService;
    #endregion

    #region Constructors
    public StudentQueryHandler(
        IStudentService studentService,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        this._studentService = studentService;
    }
    #endregion

    #region Public Methods
    public async Task<Response<List<GetAllStudentsQueryResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var studentsList = await _studentService.GetAllAsync();
        var response = _mapper.Map<List<GetAllStudentsQueryResponse>>(studentsList);

        return Success(response);
    }

    public async Task<PaginatedResponse<GetPaginatedStudentsQueryResponse>> Handle(GetPaginatedStudentsQuery request, CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
        int totalRecords = await _studentService.GetTotalCountAsync();
        var students = await _studentService.GetPaginatedListAsync(pageNumber, pageSize, request.SearchTerm, request.OrderBy);
        var studentsDto = _mapper.Map<List<GetPaginatedStudentsQueryResponse>>(students);

        var paginatedResponse = new PaginatedResponse<GetPaginatedStudentsQueryResponse>(studentsDto, pageNumber, pageSize, totalRecords);
        return paginatedResponse;
    }

    public async Task<Response<GetStudentByIdQueryResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetByIdAsync(request.Id);
        if (student is null) return NotFound<GetStudentByIdQueryResponse>(_localizer[SharedResourceKeys.NotFound]);

        var response = _mapper.Map<GetStudentByIdQueryResponse>(student);
        return Success(response);
    }
    #endregion
}