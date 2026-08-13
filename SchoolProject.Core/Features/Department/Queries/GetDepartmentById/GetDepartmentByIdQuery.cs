using System.Linq.Expressions;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.GetDepartmentById;

namespace SchoolProject.Core.Features.Department.Queries.GetDepartmentById;

public record GetDepartmentByIdQuery(int Id) : IRequest<Response<GetDepartmentByIdQueryResponse>>;