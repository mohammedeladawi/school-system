namespace SchoolProject.Application.Features.ApplicationUser.Queries.GetPaginatedUsers;

public record GetPaginatedUsersQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}