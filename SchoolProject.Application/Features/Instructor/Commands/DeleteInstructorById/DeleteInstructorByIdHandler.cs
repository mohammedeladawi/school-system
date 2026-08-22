using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands.DeleteInstructorById;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Instructor.Commands.DeleteInstructorById;

public class DeleteInstructorByIdHandler :
    BaseDeleteUserByIdHandler<
        DeleteInstructorByIdCommand,
        IInstructorManager,
        Domain.Entities.Instructor>
{
    #region Constructors
    public DeleteInstructorByIdHandler(
        IStringLocalizer<SharedResource> localizer,
        IMapper mapper,
        IInstructorManager instructorManager)
        : base(localizer, mapper, instructorManager)
    {
    }
    #endregion

}