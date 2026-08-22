using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Instructor.Commands.ChangeInstructorPassword;

public class ChangeInstructorPasswordHandler :
    BaseChangePasswordHandler<
        ChangeInstructorPasswordCommand,
        IInstructorManager,
        Domain.Entities.Instructor>
{
    #region Constructors
    public ChangeInstructorPasswordHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IInstructorManager instructorManager)
        : base(localizer, mapper, instructorManager)
    {
    }
    #endregion
}
