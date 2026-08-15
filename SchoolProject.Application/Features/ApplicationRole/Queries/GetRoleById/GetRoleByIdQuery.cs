using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationRole.Queries.GetRoleById;

namespace SchoolProject.Application.Features.ApplicationRole.Queries.GetRoleById;

public record GetRoleByIdQuery(int Id) : IRequest<Response<GetRoleByIdQueryResponse>>;