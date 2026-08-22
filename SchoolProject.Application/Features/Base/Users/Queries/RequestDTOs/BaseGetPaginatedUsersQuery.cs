using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;

namespace SchoolProject.Application.Features.Base.Users.Queries.RequestDTOs;

public record BaseGetPaginatedUsersQuery<TResponse> :
    IRequest<PaginatedResponse<TResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}