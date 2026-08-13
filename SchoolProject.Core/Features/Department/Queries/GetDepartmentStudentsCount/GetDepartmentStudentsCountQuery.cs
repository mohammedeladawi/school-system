using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.GetDepartmentStudentsCount;

namespace SchoolProject.Core.Features.Department.Queries.GetDepartmentStudentsCount;

public record GetDepartmentStudentsCountQuery : IRequest<Response<List<GetDepartmentStudentsCountQueryResponse>>>;