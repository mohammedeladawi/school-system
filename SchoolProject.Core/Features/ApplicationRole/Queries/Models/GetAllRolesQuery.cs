using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationRole.Queries.Responses;

namespace SchoolProject.Core.Features.ApplicationRole.Queries.Models;

public record GetAllRolesQuery : IRequest<Response<List<GetAllRolesQueryResponse>>>;