namespace SchoolProject.Core.Features.ApplicationUser.Queries.Responses;

public record GetApplicationUserByIdQueryResponse
{
    public string Name { get; init; } = null!;
    public string Email {get; init; } = null!;
    public string? Country {get; init; }
    public string? Phone {get; init; }
}
