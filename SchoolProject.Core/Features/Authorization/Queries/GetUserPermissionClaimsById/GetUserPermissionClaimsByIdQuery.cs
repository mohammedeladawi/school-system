using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authorization.Queries.GetUserPermissionClaimsById;

public record GetUserPermissionClaimsByIdQuery(int UserId) :
    IRequest<Response<GetUserPermissionClaimsByIdQueryResponse>>, IUserIdQuery;