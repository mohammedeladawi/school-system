using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models;

public record GetPaginatedApplicationUsersQuery :
    IRequest<PaginatedResponse<GetPaginatedApplicationUsersQueryResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}