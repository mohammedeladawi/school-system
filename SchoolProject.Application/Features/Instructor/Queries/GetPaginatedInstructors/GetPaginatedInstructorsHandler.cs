using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Queries.Handlers;
using SchoolProject.Application.Interfaces.IdentityServices;
using SchoolProject.Application.Interfaces.Repositories;
using SchoolProject.Application.Resources;

namespace SchoolProject.Application.Features.Instructor.Queries.GetPaginatedInstructors;

public class GetPaginatedInstructorsHandler :
    BaseGetPaginatedUsersHandler<
        GetPaginatedInstructorsQuery,
        GetPaginatedInstructorsResponse,
        IInstructorManager,
        Domain.Entities.Instructor>
{

    #region Constructors
    public GetPaginatedInstructorsHandler(
        IMapper mapper,
        IStringLocalizer<SharedResource> localizer,
        IInstructorManager instructorManager) :
            base(mapper, localizer, instructorManager)
    {

    }
    #endregion

}