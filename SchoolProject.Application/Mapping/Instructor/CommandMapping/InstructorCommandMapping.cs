using SchoolProject.Application.Features.Authentication.Commands.Register;
using SchoolProject.Application.Features.Instructor.Commands.AddInstructor;

namespace SchoolProject.Application.Mapping.Instructor;

public partial class InstructorProfile
{
    private void MapRegisterInstructorCommandToInstructor()
    {
        CreateMap<RegisterInstructorCommand, Domain.Entities.Instructor>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}