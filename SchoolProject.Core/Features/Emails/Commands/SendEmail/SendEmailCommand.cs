using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Emails.Commands.SendEmail;

public class SendEmailCommand : IRequest<Response<string>>
{
    public string Email { get; set; } = string.Empty;
    public string? Subject { get; set; }
    public string Message { get; set; } = string.Empty;
}