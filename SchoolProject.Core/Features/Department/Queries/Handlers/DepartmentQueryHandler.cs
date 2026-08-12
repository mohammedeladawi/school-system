using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Responses;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Department.Queries.Handlers;

public class DepartmentQueryHandler :
    ResponseHandler,
    IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdQueryResponse>>,
    IRequestHandler<GetDepartmentStudentsCountQuery, Response<List<GetDepartmentStudentsCountQueryResponse>>>
{
    #region Private Fields
    private readonly IDepartmentRepository _departmentReposIDepartmentRepository;
    #endregion

    #region Constructors
    public DepartmentQueryHandler(
        IStringLocalizer<SharedResource> localizer,
        IDepartmentRepository departmentReposIDepartmentRepository,
        IMapper mapper) : base(localizer, mapper)
    {
        _departmentReposIDepartmentRepository = departmentReposIDepartmentRepository;
    }
    #endregion

    #region Public Methods
    public async Task<Response<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new Expression<Func<Data.Entities.Department, object>>[]
        {
            d => d.Instructors,
            d => d.Students,
            d => d.Subjects
        };

        var department = await _departmentReposIDepartmentRepository.GetByIdAsync(request.Id, includes);
        if (department is null)
            return NotFound<GetDepartmentByIdQueryResponse>();

        var departmentResponse = _mapper.Map<GetDepartmentByIdQueryResponse>(department);
        return Success(departmentResponse);
    }

    public async Task<Response<List<GetDepartmentStudentsCountQueryResponse>>> Handle(GetDepartmentStudentsCountQuery request, CancellationToken cancellationToken)
    {
        var result = await _departmentReposIDepartmentRepository.GetStudentsCountViewAsync();
        var response = _mapper.Map<List<GetDepartmentStudentsCountQueryResponse>>(result);
        return Success(response);
    }
    #endregion
}