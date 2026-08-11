using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Core.Interfaces.Services;
using SchoolProject.Shared.CustomExceptions;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Commands.Validators;

public class EditStudentValidator : AbstractValidator<EditStudentCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IStudentRepository _studentRepository;
    private readonly IDepartmentRepository _departmentRepository;
    #endregion

    #region Constructors
    public EditStudentValidator(
        IStringLocalizer<SharedResource> localizer,
        IStudentRepository studentService,
        IDepartmentRepository departmentRepository)
    {
        _localizer = localizer;
        _studentRepository = studentService;
        _departmentRepository = departmentRepository;

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
            .WithMessage(_ => _localizer[SharedResourceKeys.NameArRequired])

            .MaximumLength(100)
            .WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong])

            .MustAsync(async (dto, studentNameAr, cancellationToken) =>
            {
                if (await _studentRepository.DoesNameArExistAsync(studentNameAr, dto.Id))
                    throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

                return true;
            });
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameEnRequired])

            .MaximumLength(100)
            .WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong])

            .MustAsync(async (dto, studentNameEn, cancellationToken) =>
            {
                if (await _studentRepository.DoesNameEnExistAsync(studentNameEn, dto.Id))
                    throw new ConflictException(_localizer[SharedResourceKeys.Conflict]);

                return true;
            });
    }

    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.DepartmentIdGreaterThanZero])

            .MustAsync(async (departmentId, CancellationToken) =>
                await _departmentRepository.DoesExistByIdAsync(departmentId.Value))
            .WithMessage(_ => _localizer[SharedResourceKeys.NotExist]);
    }
    #endregion
}