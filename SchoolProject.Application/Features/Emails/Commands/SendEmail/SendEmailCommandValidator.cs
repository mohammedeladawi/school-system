using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Application.Features.Emails.Commands.SendEmail;

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;

    #endregion


    #region Constructors
    public SendEmailCommandValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        ValidateEmail();
        ValidateSubject();
        ValidateMessage();
    }
    #endregion

    #region Private Methods
    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
           .NotEmpty()
           .WithMessage(_localizer[SharedResourceKeys.Required])

           .EmailAddress()
           .WithMessage(_localizer[SharedResourceKeys.InvalidEmailAddress]);
    }

    private void ValidateSubject()
    {
        RuleFor(x => x.Subject)
            .MaximumLength(200)
            .WithMessage(_localizer[SharedResourceKeys.SubjectTooLong])
            .When(x => !string.IsNullOrEmpty(x.Subject));
    }

    private void ValidateMessage()
    {
        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.Required]);
    }

    #endregion
}