using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Instructor.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Instructor.Commands.Validators;

public class AddInstructorValidator : AbstractValidator<AddInstructorCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public AddInstructorValidator(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;

        ValidateNameEn();
        ValidateNameAr();
        ValidateDepartmentId();
        ValidateSupervisorId();
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage(_ => _localizer[SharedResourceKeys.NameArRequired])
            .MaximumLength(100).WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong]);
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage(_ => _localizer[SharedResourceKeys.NameEnRequired])
            .MaximumLength(100).WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong]);
    }

    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.DepartmentIdGreaterThanZero]);
    }

    private void ValidateSupervisorId()
    {
        RuleFor(x => x.SupervisorId)
            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.DepartmentIdGreaterThanZero]);
    }
}