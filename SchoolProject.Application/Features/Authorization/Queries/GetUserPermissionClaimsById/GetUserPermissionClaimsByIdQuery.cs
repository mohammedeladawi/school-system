using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authorization.Queries.GetUserPermissionClaimsById;

public record GetUserPermissionClaimsByIdQuery(int UserId) :
    IRequest<Response<GetUserPermissionClaimsByIdQueryResponse>>, IUserIdQuery;