using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationRole.Queries.Responses;

namespace SchoolProject.Core.Features.ApplicationRole.Queries.Models;

public record GetRoleByIdQuery(int Id) : IRequest<Response<GetRoleByIdQueryResponse>>;