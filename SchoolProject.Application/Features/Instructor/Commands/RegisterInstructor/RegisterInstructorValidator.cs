using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Resources;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate;
using SchoolProject.Application.Features.ApplicationUser.Commands;
using SchoolProject.Application.Helpers;

namespace SchoolProject.Application.Features.Instructor.Commands.RegisterInstructor;

public class RegisterInstructorValidator : AbstractValidator<RegisterInstructorCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IInstructorRepository _instructrorRepository;
    private readonly IUserManager _userManager;
    #endregion

    #region Constructors
    public RegisterInstructorValidator(
        IStringLocalizer<SharedResource> localizer,
        IDepartmentRepository departmentRepository,
        IInstructorRepository instructrorRepository,
        IUserManager userManager)
    {
        _localizer = localizer;
        _departmentRepository = departmentRepository;
        _instructrorRepository = instructrorRepository;
        _userManager = userManager;

        Include(new CommonUserCommandValidator(localizer));

        ValidateDepartmentId();
        ValidateSupervisorId();
        ValidatePassword();
        ValidateEmail();
        ValidateUserName();
    }
    #endregion

    #region Private Methods

    private void ValidateEmail()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailRequired])

            .Matches(RegxPatterns.EmailPattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailInvalid])

            .MustAsync(async (email, cancellationToken) =>
                !await _userManager.DoesEmailExist(email))
            .WithMessage(_ => _localizer[SharedResourceKeys.EmailAlreadyInUse]);
    }

    private void ValidateUserName()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameRequired])

            .MaximumLength(50)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameTooLong])

            .Matches(RegxPatterns.UserNamePattern)
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameInvalid])

            .MustAsync(async (userName, cancellationToken) =>
                !await _userManager.DoesUserNameExist(userName))
            .WithMessage(_ => _localizer[SharedResourceKeys.UserNameAlreadyInUse]);
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

    private void ValidatePassword()
    {
        RuleFor(x => x.Password)
            .ValidatePassword(_localizer);

        RuleFor(x => x.ConfirmPassword)
            .ValidateConfirmPassword(x => x.Password, _localizer);
    }

    #endregion

}
