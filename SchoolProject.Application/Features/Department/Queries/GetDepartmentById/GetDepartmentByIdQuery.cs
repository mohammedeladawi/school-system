using System.Linq.Expressions;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Department.Queries.GetDepartmentById;

namespace SchoolProject.Application.Features.Department.Queries.GetDepartmentById;

public record GetDepartmentByIdQuery(int Id) : IRequest<Response<GetDepartmentByIdQueryResponse>>;