using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Service.Abstracts;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Commands.Validators;

public class EditStudentValidator : AbstractValidator<EditStudentCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IStudentService _studentService;
    private readonly IDepartmentService _departmentService;
    #endregion

    #region Constructors
    public EditStudentValidator(
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
    #endregion

    #region Private Methods
    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.NameArRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameTooLong])

            .MustAsync(async (dto, studentNameAr, cancellationToken) =>
            {
                if (await _studentService.DoesNameArExistAsync(studentNameAr, dto.Id))
                    throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

                return true;
            });
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourceKeys.NameEnRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameTooLong])

            .MustAsync(async (dto, studentNameEn, cancellationToken) =>
            {
                if (await _studentService.DoesNameEnExistAsync(studentNameEn, dto.Id))
                    throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

                return true;
            });
    }
    
    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.DepartmentIdGreaterThanZero])

            .MustAsync(async (departmentId, CancellationToken) =>
                await _departmentService.DoesExistByIdAsync(departmentId.Value))
            .WithMessage(_localizer[SharedResourceKeys.NotExist]);
    }
    #endregion
}