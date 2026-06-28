namespace SchoolProject.Core.Features.Authentication.Commands.Responses;

public class AuthResponse
{
    public string JwtToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
