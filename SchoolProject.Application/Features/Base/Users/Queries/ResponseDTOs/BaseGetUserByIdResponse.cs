namespace SchoolProject.Application.Features.Base.ApplicationUser.Queries.ResponseDTOs;

public record BaseGetUserByIdResponse
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string? Address { get; init; }
    public string? PhoneNumber { get; init; }
}