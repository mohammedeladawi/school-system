namespace SchoolProject.Shared.Helpers;

public static class RegxPatterns
{
    public const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    public const string PhonePattern = @"^\+?[1-9]\d{1,14}$";
    public const string UserNamePattern = @"^[a-zA-Z0-9_]$";
    public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
}