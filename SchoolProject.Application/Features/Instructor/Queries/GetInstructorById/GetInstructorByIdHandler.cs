using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Queries.GetUserById;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Instructor.Queries.GetInstructorById;

public class GetInstructorByIdHandler :
    BaseGetUserByIdHandler<
        GetInstructorByIdQuery,
        GetInstructorByIdResponse,
        IInstructorManager,
        Domain.Entities.Instructor>
{
    #region Constructors
    public GetInstructorByIdHandler(
        IInstructorManager instructorManager,
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer) : base(instructorManager, mapper, localizer)
    {
    }
    #endregion
}