using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.GetPaginatedUsers;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.GetPaginatedUsers;

public record GetPaginatedUsersQuery :
    IRequest<PaginatedResponse<GetPaginatedUsersQueryResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}