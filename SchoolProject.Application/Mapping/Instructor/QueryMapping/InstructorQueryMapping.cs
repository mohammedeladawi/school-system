using SchoolProject.Application.Features.ApplicationUser.Commands.EditInstructor;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;
using SchoolProject.Application.Features.Instructor.Commands.RegisterInstructor;
using SchoolProject.Application.Features.Instructor.Queries.GetInstructorById;
using SchoolProject.Application.Features.Instructor.Queries.GetPaginatedInstructors;

namespace SchoolProject.Application.Mapping.Instructor;

public partial class InstructorProfile
{
    private void MapInstructorToGetPaginatedInstructorsResponse()
    {
        CreateMap<
            Domain.Entities.Instructor,
            GetPaginatedInstructorsResponse>();
    }

    private void MapInstructorToGetInstructorByIdResponse()
    {
        CreateMap<
            Domain.Entities.Instructor,
            GetInstructorByIdResponse>();
    }
}