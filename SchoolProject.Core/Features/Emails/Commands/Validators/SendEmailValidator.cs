using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Emails.Commands.Validators;

public class SendEmailValidator : AbstractValidator<SendEmailCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public SendEmailValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.Required])
            .EmailAddress().WithMessage(_localizer[SharedResourceKeys.InvalidEmailAddress]);

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.Required]);
    }
}
