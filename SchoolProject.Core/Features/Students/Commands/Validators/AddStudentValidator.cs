using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validators;

public class AddStudentValidator : AbstractValidator<AddStudentCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IStudentService _studentService;
    private readonly IDepartmentService _departmentService;

    public AddStudentValidator(
        IStringLocalizer<SharedResource> localizer,
        IStudentService studentService,
        IDepartmentService departmentService)
    {
        _localizer = localizer;
        _studentService = studentService;
        _departmentService = departmentService;

        ValidateNameEn();
        ValidateNameAr();
        ValidatePhone();
        ValidateAddress();
        ValidateDepartmentId();
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.NameArRequired])
            .MaximumLength(100).WithMessage(_localizer[SharedResourceKeys.NameTooLong])

            .MustAsync(async (studentNameAr, cancellationToken) =>
            {
                if (await _studentService.IsStudentNameArExistAsync(studentNameAr))
                    throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

                return true;
            });
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage(_localizer[SharedResourceKeys.NameEnRequired])
            .MaximumLength(100).WithMessage(_localizer[SharedResourceKeys.NameTooLong])
            .MustAsync(async (studentNameEn, cancellationToken) =>
            {
                if (await _studentService.IsStudentNameEnExistAsync(studentNameEn))
                    throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

                return true;
            });
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
            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.DepartmentIdGreaterThanZero])

            .MustAsync(async (departmentId, cancellationToken) =>
                await _departmentService.IsExistByIdAsync(departmentId!.Value))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);

    }
}