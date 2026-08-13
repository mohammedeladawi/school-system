using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Commands.EditStudent;

public class EditStudentCommandValidator : AbstractValidator<EditStudentCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IStudentRepository _studentRepository;
    #endregion

    #region Constructors
    public EditStudentCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IDepartmentRepository departmentRepository,
        IStudentRepository studentRepository)
    {
        _localizer = localizer;
        _studentRepository = studentRepository;

        Include(new CommonStudentCommandValidator(localizer, departmentRepository));

        ValidateId();
        ValidateNameAr();
        ValidateNameEn();
    }
    #endregion

    #region Private Methods
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(_ => _localizer[SharedResourceKeys.IdGreaterThanZero])

            .MustAsync(async (id, cancellationToken) =>
                await _studentRepository.DoesExistByIdAsync(id))
            .WithMessage(_localizer[SharedResourceKeys.NotFound]);
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameEnRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameEnTooLong])

            .MustAsync(async (student, nameEn, cancellationToken) =>
                !await _studentRepository.DoesNameEnExistAsync(nameEn, student.Id))
            .WithMessage(_localizer[SharedResourceKeys.NameEnAlreadyInUse]);
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameArRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameArTooLong])

            .MustAsync(async (student, nameAr, cancellationToken) =>
                !await _studentRepository.DoesNameArExistAsync(nameAr, student.Id))
            .WithMessage(_localizer[SharedResourceKeys.NameArAlreadyInUse]);

    }
    #endregion
}