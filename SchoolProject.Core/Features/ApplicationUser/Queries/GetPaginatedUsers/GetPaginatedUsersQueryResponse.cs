namespace SchoolProject.Core.Features.ApplicationUser.Queries.GetPaginatedUsers;

public record GetPaginatedUsersQueryResponse
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Country { get; set; }
    public string? Phone { get; set; }
}