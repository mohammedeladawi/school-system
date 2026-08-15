using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Department.Queries.GetDepartmentStudentsCount;

public class GetDepartmentStudentsCountHandler : ResponseHandler, IRequestHandler<GetDepartmentStudentsCountQuery, Response<List<GetDepartmentStudentsCountQueryResponse>>>
{
    #region Private Fields
    private readonly IDepartmentRepository _departmentRepository;
    #endregion

    #region Constructors
    public GetDepartmentStudentsCountHandler(
        IStringLocalizer<SharedResource> localizer,
        IDepartmentRepository departmentRepository,
        IMapper mapper) : base(localizer, mapper)
    {
        _departmentRepository = departmentRepository;
    }
    #endregion

    #region Public Methods
    public async Task<Response<List<GetDepartmentStudentsCountQueryResponse>>> Handle(GetDepartmentStudentsCountQuery request, CancellationToken cancellationToken)
    {
        var result = await _departmentRepository.GetStudentsCountViewAsync();
        var response = _mapper.Map<List<GetDepartmentStudentsCountQueryResponse>>(result);
        return Success(response);
    }
    #endregion
}