using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Commands.ChangeStudentPassword;

public class ChangeStudentPasswordCommandValidator :
    AbstractValidator<ChangeStudentPasswordCommand>
{
    #region Private Fields
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IStudentManager _studentManager;
    #endregion

    #region Constructors
    public ChangeStudentPasswordCommandValidator(
        IStringLocalizer<SharedResource> localizer,
        IStudentManager studentManager)
    {
        _localizer = localizer;
        _studentManager = studentManager;

        Include(new BaseChangePasswordCommandValidator<IStudentManager, Domain.Entities.Student>(_localizer, _studentManager));
    }
    #endregion
}
