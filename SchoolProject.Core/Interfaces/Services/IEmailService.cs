namespace SchoolProject.Core.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string email, string message, string? reason = null);
}
