using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Responses;
using SchoolProject.Shared.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Department.Queries.Handlers;

public class DepartmentQueryHandler :
    ResponseHandler,
    IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdQueryResponse>>
{
    #region Private Fields
    private readonly IDepartmentService _departmentService;
    #endregion

    #region Constructors
    public DepartmentQueryHandler(
        IStringLocalizer<SharedResource> localizer,
        IDepartmentService departmentService,
        IMapper mapper) : base(localizer, mapper)
    {
        _departmentService = departmentService;
    }
    #endregion

    #region Public Methods
    public async Task<Response<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _departmentService.GetByIdAsync(request.Id);
        if (department is null)
            return NotFound<GetDepartmentByIdQueryResponse>();

        var departmentResponse = _mapper.Map<GetDepartmentByIdQueryResponse>(department);
        return Success(departmentResponse);
    }
    #endregion
}