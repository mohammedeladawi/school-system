using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Domain.Entities;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Department.Queries.GetDepartmentById;

public class GetDepartmentByIdHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdQueryResponse>>
{
    #region Private Fields
    private readonly IDepartmentRepository _departmentRepository;

    #endregion

    #region Constructors
    public GetDepartmentByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IDepartmentRepository departmentRepository,
        IMapper mapper) : base(localizer, mapper)
    {
        _departmentRepository = departmentRepository;
    }
    #endregion

    #region Public Methods
    public async Task<Response<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new Expression<Func<Domain.Entities.Department, object>>[]
        {
            d => d.Instructors,
            d => d.Students,
            d => d.Subjects
        };

        var department = await _departmentRepository.GetByIdAsync(request.Id, includes);
        if (department is null)
            return NotFound<GetDepartmentByIdQueryResponse>();

        var departmentResponse = _mapper.Map<GetDepartmentByIdQueryResponse>(department);
        return Success(departmentResponse);
    }

    #endregion
}