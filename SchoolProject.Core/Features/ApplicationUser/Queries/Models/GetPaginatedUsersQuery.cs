using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models;

public record GetPaginatedUsersQuery :
    IRequest<PaginatedResponse<GetPaginatedUsersQueryResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}