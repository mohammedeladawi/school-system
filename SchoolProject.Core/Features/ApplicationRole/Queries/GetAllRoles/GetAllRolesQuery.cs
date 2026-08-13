using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationRole.Queries.GetAllRoles;

namespace SchoolProject.Core.Features.ApplicationRole.Queries.GetAllRoles;

public record GetAllRolesQuery : IRequest<Response<List<GetAllRolesQueryResponse>>>;