using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationRole.Queries.GetAllRoles;

namespace SchoolProject.Application.Features.ApplicationRole.Queries.GetAllRoles;

public record GetAllRolesQuery : IRequest<Response<List<GetAllRolesQueryResponse>>>;