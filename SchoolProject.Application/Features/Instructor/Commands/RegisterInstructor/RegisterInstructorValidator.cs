using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Features.Authentication.Commands.Register;

namespace SchoolProject.Application.Features.Instructor.Commands.RegisterInstructor;

public class RegisterInstructorValidator : AbstractValidator<RegisterInstructorCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IInstructorRepository _instructrorRepository;

    #endregion

    #region Constructors
    public RegisterInstructorValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager,
        IDepartmentRepository departmentRepository,
        IInstructorRepository instructrorRepository)
    {
        _localizer = localizer;
        _departmentRepository = departmentRepository;
        _instructrorRepository = instructrorRepository;

        Include(new CommonRegisterValidator(localizer, userManager));

        ValidateDepartmentId();
        ValidateSupervisorId();
    }
    #endregion

    #region Private Methods

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
    #endregion

}
