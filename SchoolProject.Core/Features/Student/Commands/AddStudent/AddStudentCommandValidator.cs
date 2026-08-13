using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Student.Commands.AddStudent;

public class AddStudentCommandValidator : AbstractValidator<AddStudentCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IStudentRepository _studentRepository;
    #endregion

    #region Constructors
    public AddStudentCommandValidator(
        IStudentRepository studentRepository,
        IStringLocalizer<SharedResource> localizer,
        IDepartmentRepository departmentRepository)
    {
        _localizer = localizer;
        _studentRepository = studentRepository;

        Include(new CommonStudentCommandValidator(localizer, departmentRepository));

        ValidateNameAr();
        ValidateNameEn();
    }
    #endregion

    #region Private Methods
    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameEnRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameEnTooLong])

            .MustAsync(async (nameEn, cancellationToken) =>
                !await _studentRepository.DoesNameEnExistAsync(nameEn))
            .WithMessage(_localizer[SharedResourceKeys.NameEnAlreadyInUse]);
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameArRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameArTooLong])

            .MustAsync(async (nameAr, cancellationToken) =>
                !await _studentRepository.DoesNameArExistAsync(nameAr))
            .WithMessage(_localizer[SharedResourceKeys.NameArAlreadyInUse]);

    }
    #endregion
}