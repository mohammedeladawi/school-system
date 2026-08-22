using AutoMapper;
namespace SchoolProject.Application.Mapping.Instructor;


public partial class InstructorProfile : Profile
{
    public InstructorProfile()
    {
        MapRegisterInstructorCommandToInstructor();
        MapEditInstructorCommandToInstructor();
        MapInstructorToGetPaginatedInstructorsResponse();
        MapInstructorToGetInstructorByIdResponse();
    }
}


