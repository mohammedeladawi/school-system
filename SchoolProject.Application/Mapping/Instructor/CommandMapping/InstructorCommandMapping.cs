using SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate;
using SchoolProject.Application.Features.Instructor.Commands.RegisterInstructor;

namespace SchoolProject.Application.Mapping.Instructor;

public partial class InstructorProfile
{
    private void MapRegisterInstructorCommandToInstructor()
    {
        CreateMap<RegisterInstructorCommand, Domain.Entities.Instructor>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}