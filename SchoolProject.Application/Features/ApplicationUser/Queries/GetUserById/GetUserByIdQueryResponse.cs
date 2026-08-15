namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;

public record GetUserByIdQueryResponse
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string? Country { get; init; }
    public string? Phone { get; init; }
}