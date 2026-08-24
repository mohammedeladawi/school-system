using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;
using SchoolProject.Application.Helpers;
using SchoolProject.Application.Helpers.Validations;

namespace SchoolProject.Application.Features.Student.Commands.RegisterStudent;

public class RegisterStudentValidator : AbstractValidator<RegisterStudentCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public RegisterStudentValidator(
        IStringLocalizer<SharedResource> localizer,
        IUserManager userManager,
        IDepartmentRepository departmentRepository)
    {
        _localizer = localizer;
        _departmentRepository = departmentRepository;
        _userManager = userManager;

        Include(new BaseUserCommandValidator(localizer));
        ValidateDepartmentId();
        ValidatePassword();
        ValidateEmail();
        ValidateUserName();
    }
    #endregion

    #region Private Methods

    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .ValidateEmail(_localizer, _userManager);
    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .ValidateUserName(_localizer, _userManager);
    }

    private void ValidateDepartmentId()
    {
        RuleFor(x => x.DepartmentId)
            .ValidateDepartmentId(_localizer, _departmentRepository.DoesExistByIdAsync);
    }

    private void ValidatePassword()
    {
        RuleFor(x => x.Password)
            .ValidatePassword(_localizer);

        RuleFor(x => x.ConfirmPassword)
            .ValidateConfirmPassword(x => x.Password, _localizer);
    }

    #endregion
}
