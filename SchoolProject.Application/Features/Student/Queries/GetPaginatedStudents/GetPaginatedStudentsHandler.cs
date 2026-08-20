using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;

public class GetPaginatedStudentsHandler : ResponseHandler, IRequestHandler<GetPaginatedStudentsQuery, PaginatedResponse<GetPaginatedStudentsQueryResponse>>
{
    #region Private Fields
    private readonly IStudentManager _StudentManager;
    #endregion

    #region Constructors
    public GetPaginatedStudentsHandler(
        IStudentManager StudentManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _StudentManager = StudentManager;
    }
    #endregion

    #region Public Methods
    public async Task<PaginatedResponse<GetPaginatedStudentsQueryResponse>> Handle(GetPaginatedStudentsQuery request, CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = (request.PageSize <= 0 || request.PageSize >= 20) ? 20 : request.PageSize;
        int totalRecords = await _StudentManager.GetTotalCountAsync();
        var students = await _StudentManager.GetPaginatedListAsync(pageNumber, pageSize, [s => s.Department]);
        var studentsDto = _mapper.Map<List<GetPaginatedStudentsQueryResponse>>(students);

        var paginatedResponse = new PaginatedResponse<GetPaginatedStudentsQueryResponse>(studentsDto, pageNumber, pageSize, totalRecords);
        return paginatedResponse;
    }
    #endregion
}