using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Commands.Validators;

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
        ValidateDepartmentId();

        Include(new CommonStudentCommandValidator(localizer));
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage(_ => _localizer[SharedResourceKeys.NameArRequired])
            .MaximumLength(100).WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong])

            .MustAsync(async (studentNameAr, cancellationToken) =>
            {
                if (await _studentService.DoesNameArExistAsync(studentNameAr))
                    throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

                return true;
            });
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage(_ => _localizer[SharedResourceKeys.NameEnRequired])
            .MaximumLength(100).WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong])
            .MustAsync(async (studentNameEn, cancellationToken) =>
            {
                if (await _studentService.DoesNameEnExistAsync(studentNameEn))
                    throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

                return true;
            });
    }

    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.DepartmentIdGreaterThanZero])

            .MustAsync(async (departmentId, cancellationToken) =>
                await _departmentService.DoesExistByIdAsync(departmentId!.Value))
            .WithMessage(_ => _localizer[SharedResourceKeys.NotExist]);

    }
}