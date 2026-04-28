using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Students.Commands.Validators;

public class EditStudentValidator : AbstractValidator<EditStudentCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public EditStudentValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
        ValidateName();
        ValidatePhone();
        ValidateAddress();
        ValidateDepartmentId();
    }

    private void ValidateName()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.NameRequired])
            .MaximumLength(100).WithMessage(_localizer[SharedResourceKeys.NameTooLong]);
    }

    private void ValidatePhone()
    {
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.PhoneRequired])
            .MaximumLength(20).WithMessage(_localizer[SharedResourceKeys.PhoneTooLong]);
    }

    private void ValidateAddress()
    {
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.AddressRequired])
            .MaximumLength(200).WithMessage(_localizer[SharedResourceKeys.AddressTooLong]);
    }

    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage(_localizer[SharedResourceKeys.DepartmentIdGreaterThanZero]);
    }
}