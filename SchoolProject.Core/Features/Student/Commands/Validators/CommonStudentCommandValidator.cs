using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Commands.Validators;

public class CommonStudentCommandValidator : AbstractValidator<CommonStudentDto>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    #endregion

    #region Constructors
    public CommonStudentCommandValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        ValidatePhone();
        ValidateAddress();

    }
    #endregion

    #region Private Methods
    private void ValidatePhone()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.PhoneRequired])

            .MaximumLength(20)
            .WithMessage(_localizer[SharedResourceKeys.PhoneTooLong]);
    }
    private void ValidateAddress()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.AddressRequired])

            .MaximumLength(200)
            .WithMessage(_localizer[SharedResourceKeys.AddressTooLong]);
    }
    #endregion
}
