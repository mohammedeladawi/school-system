namespace SchoolProject.Application.Features.Base.Users.Queries.ResponseDTOs;

public record BaseGetPaginatedUsersResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}