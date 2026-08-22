using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Helpers.Validations;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditInstructor;

public class EditInstructorCommandValidator :
    AbstractValidator<EditInstructorCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IInstructorManager _instructorManager;
    #endregion

    #region Constructors
    public EditInstructorCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager,
        IDepartmentRepository departmentRepository,
        IInstructorManager instructorManager)
    {
        _localizer = localizer;
        _userManager = userManager;
        _departmentRepository = departmentRepository;
        _instructorManager = instructorManager;

        Include(new BaseUserCommandValidator(localizer));

        ValidateId();
        ValidateEmail();
        ValidateUserName();
        ValidateDepartmentId();
        ValidateSupervisorId();
    }
    #endregion

    #region Private Methods
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .ValidateUserId(_localizer, _instructorManager.DoesExistByIdAsync);
    }

    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .ValidateEmail(_localizer, _userManager, x => x.Id);
    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .ValidateUserName(_localizer, _userManager, x => x.Id);
    }

    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .ValidateDepartmentId(_localizer, _departmentRepository.DoesExistByIdAsync);
    }

    private void ValidateSupervisorId()
    {
        RuleFor(x => x.SupervisorId)
            .ValidateSupervisorId(_localizer, _instructorManager.DoesExistByIdAsync);
    }


    #endregion
}