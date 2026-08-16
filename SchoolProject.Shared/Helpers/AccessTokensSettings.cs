namespace SchoolProject.Shared.Helpers;

public class AccessTokensSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string DurationInMinutes { get; set; } = string.Empty;
    public short RefreshTokenInMinutes { get; set; }
}
