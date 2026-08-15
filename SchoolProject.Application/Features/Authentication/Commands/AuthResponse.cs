namespace SchoolProject.Application.Features.Authentication.Commands;

public class AuthResponse
{
    public string JwtToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
