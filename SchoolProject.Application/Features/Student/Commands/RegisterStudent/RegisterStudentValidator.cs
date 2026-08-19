using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Features.Authentication.Commands.Register;

namespace SchoolProject.Application.Features.Student.Commands.RegisterStudent;

public class RegisterStudentValidator : AbstractValidator<RegisterStudentCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IDepartmentRepository _departmentRepository;
    #endregion

    #region Constructors
    public RegisterStudentValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager,
        IDepartmentRepository departmentRepository)
    {
        _localizer = localizer;
        _departmentRepository = departmentRepository;

        Include(new CommonRegisterValidator(localizer, userManager));
        ValidateDepartmentId();
    }
    #endregion

    #region Private Methods
    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .WithMessage(_localizer[SharedResourceKeys.DepartmentIdGreaterThanZero])

            .MustAsync(async (departmentId, cancellationToken) =>
                await _departmentRepository.DoesExistByIdAsync(departmentId))
            .WithMessage(_localizer[SharedResourceKeys.DepartmentDoesNotExist]);
    }

    #endregion

}
