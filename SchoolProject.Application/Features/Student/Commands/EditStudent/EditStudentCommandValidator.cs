using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Commands.EditStudent;

public class EditStudentCommandValidator : AbstractValidator<EditStudentCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IStudentManager _StudentManager;
    #endregion

    #region Constructors
    public EditStudentCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IDepartmentRepository departmentRepository,
        IStudentManager StudentManager)
    {
        _localizer = localizer;
        _StudentManager = StudentManager;

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
                await _StudentManager.DoesExistByIdAsync(id))
            .WithMessage(_localizer[SharedResourceKeys.NotFound]);
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameEnRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameEnTooLong]);
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameArRequired])

            .MaximumLength(100)
            .WithMessage(_localizer[SharedResourceKeys.NameArTooLong]);
    }
    #endregion
}