using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Queries.GetPaginatedStudents;

public class GetPaginatedStudentsHandler : ResponseHandler, IRequestHandler<GetPaginatedStudentsQuery, PaginatedResponse<GetPaginatedStudentsQueryResponse>>
{
    #region Private Fields
    private readonly IStudentRepository _studentRepository;
    #endregion

    #region Constructors
    public GetPaginatedStudentsHandler(
        IStudentRepository studentRepository,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(localizer, mapper)
    {
        _studentRepository = studentRepository;
    }
    #endregion

    #region Public Methods
    public async Task<PaginatedResponse<GetPaginatedStudentsQueryResponse>> Handle(GetPaginatedStudentsQuery request, CancellationToken cancellationToken)
    {
        int pageNumber = request.PageNumber < 0 ? 1 : request.PageNumber;
        int pageSize = (request.PageSize <= 0 || request.PageSize >= 20) ? 20 : request.PageSize;
        int totalRecords = await _studentRepository.GetTotalCountAsync();
        var students = await _studentRepository.GetPaginatedListAsync(pageNumber, pageSize, [s => s.Department]);
        var studentsDto = _mapper.Map<List<GetPaginatedStudentsQueryResponse>>(students);

        var paginatedResponse = new PaginatedResponse<GetPaginatedStudentsQueryResponse>(studentsDto, pageNumber, pageSize, totalRecords);
        return paginatedResponse;
    }
    #endregion
}