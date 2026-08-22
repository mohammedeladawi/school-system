using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;
using SchoolProject.Application.Helpers.Validations;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Commands.EditStudent;

public class EditStudentCommandValidator :
    AbstractValidator<EditStudentCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserManager _userManager;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IStudentManager _studentManager;
    #endregion

    #region Constructors
    public EditStudentCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager,
        IDepartmentRepository departmentRepository,
        IStudentManager studentManager)
    {
        _localizer = localizer;
        _userManager = userManager;
        _departmentRepository = departmentRepository;
        _studentManager = studentManager;

        Include(new BaseUserCommandValidator(localizer));

        ValidateId();
        ValidateEmail();
        ValidateUserName();
        ValidateDepartmentId();
    }
    #endregion

    #region Private Methods
    private void ValidateId()
    {
        RuleFor(x => x.Id)
            .ValidateUserId(_localizer, _studentManager.DoesExistByIdAsync);
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
    #endregion
}