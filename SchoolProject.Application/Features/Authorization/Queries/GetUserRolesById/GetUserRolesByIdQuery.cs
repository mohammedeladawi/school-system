using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authorization.Queries.GetUserRolesById;

public record GetUserRolesByIdQuery(int UserId) :
    IRequest<Response<GetUserRolesByIdQueryResponse>>, IUserIdQuery;