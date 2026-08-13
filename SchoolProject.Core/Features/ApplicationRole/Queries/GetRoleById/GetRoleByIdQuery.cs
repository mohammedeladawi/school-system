using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationRole.Queries.GetRoleById;

namespace SchoolProject.Core.Features.ApplicationRole.Queries.GetRoleById;

public record GetRoleByIdQuery(int Id) : IRequest<Response<GetRoleByIdQueryResponse>>;