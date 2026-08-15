using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;

namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;

public record GetPaginatedUsersQuery :
    IRequest<PaginatedResponse<GetPaginatedUsersQueryResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}