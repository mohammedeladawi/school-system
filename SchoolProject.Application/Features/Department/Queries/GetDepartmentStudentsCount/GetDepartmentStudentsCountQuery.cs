using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Department.Queries.GetDepartmentStudentsCount;

namespace SchoolProject.Application.Features.Department.Queries.GetDepartmentStudentsCount;

public record GetDepartmentStudentsCountQuery : IRequest<Response<List<GetDepartmentStudentsCountQueryResponse>>>;