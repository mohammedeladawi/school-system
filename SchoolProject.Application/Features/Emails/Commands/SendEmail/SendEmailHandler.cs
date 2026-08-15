using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Interfaces.Services;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Emails.Commands.SendEmail;

public class SendEmailHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
{
    private readonly IEmailService _emailService;

    public SendEmailHandler(
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer,
        IEmailService emailService) : base(localizer, mapper)
    {
        _emailService = emailService;
    }

    public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailAsync(request.Email, request.Message, request.Subject);
        return Success<string>(_localizer[SharedResourceKeys.EmailSentSuccessfully]);
    }
}