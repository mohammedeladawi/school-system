using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Instructor.Commands.ChangeInstructorPassword;

public class ChangeInstructorPasswordCommandValidator :
    AbstractValidator<ChangeInstructorPasswordCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IInstructorManager _instructorManager;
    #endregion

    #region Constructors
    public ChangeInstructorPasswordCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IInstructorManager instructorManager)
    {
        _localizer = localizer;
        _instructorManager = instructorManager;

        Include(new BaseChangePasswordCommandValidator<IInstructorManager, Domain.Entities.Instructor>(_localizer, _instructorManager));
    }
    #endregion
}
