using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Emails.Commands.Handlers;

public class EmailCommandHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
{
    private readonly IEmailService _emailService;

    public EmailCommandHandler(
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer,
        IEmailService emailService) : base(localizer, mapper)
    {
        _emailService = emailService;
    }

    public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailAsync(request.Email, request.Message, request.Reason);
        return Success<string>(_localizer[SharedResourceKeys.EmailSentSuccessfully]);
    }
}
