namespace SchoolProject.Application.Helpers.ConfigBinders;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string AccessTokenDurationInMinutes { get; set; } = string.Empty;
    public short RefreshTokenDurationInMinutes { get; set; }
}
