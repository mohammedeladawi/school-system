using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authorization.Queries.GetUserRolesById;

public record GetUserRolesByIdQuery(int UserId) :
    IRequest<Response<GetUserRolesByIdQueryResponse>>, IUserIdQuery;