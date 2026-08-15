using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Emails.Commands.SendEmail;

public class SendEmailCommand : IRequest<Response<string>>
{
    public string Email { get; set; } = string.Empty;
    public string? Subject { get; set; }
    public string Message { get; set; } = string.Empty;
}