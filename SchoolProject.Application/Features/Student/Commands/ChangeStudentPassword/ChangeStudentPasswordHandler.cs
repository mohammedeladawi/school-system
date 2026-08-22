using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Student.Commands.ChangeStudentPassword;

public class ChangeStudentPasswordHandler :
    BaseChangePasswordHandler<
        ChangeStudentPasswordCommand,
        IStudentManager,
        Domain.Entities.Student>
{
    #region Constructors
    public ChangeStudentPasswordHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IStudentManager studentManager)
        : base(localizer, mapper, studentManager)
    {
    }
    #endregion
}
