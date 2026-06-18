using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Responses;

namespace SchoolProject.Core.Features.Department.Queries.Models;

public record GetDepartmentByIdQuery(int Id) : IRequest<Response<GetDepartmentByIdQueryResponse>>;