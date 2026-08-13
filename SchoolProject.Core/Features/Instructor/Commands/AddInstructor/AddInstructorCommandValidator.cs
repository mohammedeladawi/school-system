using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Interfaces.Repositories;
using SchoolProject.Shared.Resources;

namespace SchoolProject.Core.Features.Instructor.Commands.AddInstructor;

public class AddInstructorCommandValidator : AbstractValidator<AddInstructorCommand>
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IInstructorRepository _instructrorRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public AddInstructorCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IInstructorRepository instructorRepository,
        IDepartmentRepository departmentRepository)
    {
        _localizer = localizer;
        _instructrorRepository = instructorRepository;
        _departmentRepository = departmentRepository;

        ValidateNameEn();
        ValidateNameAr();
        ValidateDepartmentId();
        ValidateSupervisorId();
    }

    private void ValidateNameAr()
    {
        RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameArRequired])

            .MaximumLength(100)
            .WithMessage(_ => _localizer[SharedResourceKeys.NameArTooLong])

            .MustAsync(async (nameAr, cancellationToken) =>
                !await _instructrorRepository.DoesNameArExistAsync(nameAr))
            .WithMessage(_ => _localizer[SharedResourceKeys.NameArAlreadyInUse]);
    }

    private void ValidateNameEn()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.NameEnRequired])

            .MaximumLength(100)
            .WithMessage(_ => _localizer[SharedResourceKeys.NameTooLong])

            .MustAsync(async (nameEn, cancellationToken) =>
                !await _instructrorRepository.DoesNameEnExistAsync(nameEn))
            .WithMessage(_ => _localizer[SharedResourceKeys.NameEnAlreadyInUse]);

    }

    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .WithMessage(_ => _localizer[
                SharedResourceKeys.DepartmentIdGreaterThanZero])
            .When(x => x.DepartmentId.HasValue);

        RuleFor(x => x.DepartmentId)
            .MustAsync(async (departmentId, cancellationToken) =>
                await _departmentRepository.DoesExistByIdAsync(
                    departmentId!.Value))
            .WithMessage(_ => _localizer[
                SharedResourceKeys.DepartmentNotExist])
            .When(x => x.DepartmentId.HasValue);
    }

    private void ValidateSupervisorId()
    {
        RuleFor(x => x.SupervisorId)
            .GreaterThan(0)
            .WithMessage(_ => _localizer[
                SharedResourceKeys.SupervisorIdGreaterThanZero])
            .When(x => x.SupervisorId.HasValue);

        RuleFor(x => x.SupervisorId)
            .MustAsync(async (supervisorId, cancellationToken) =>
                await _instructrorRepository.DoesExistByIdAsync(
                    supervisorId!.Value))
            .WithMessage(_ => _localizer[
                SharedResourceKeys.SupervisorNotExist])
            .When(x => x.SupervisorId.HasValue);
    }
}