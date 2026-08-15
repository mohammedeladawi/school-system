using SchoolProject.Application.Features.Instructor.Commands.AddInstructor;

namespace SchoolProject.Application.Mapping.Instructor;

public partial class InstructorProfile
{
    private void MapAddInstructorCommandToInstructor()
    {
        CreateMap<AddInstructorCommand, Domain.Entities.Instructor>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .ForSourceMember(src => src.Image, opt => opt.DoNotValidate());
    }
}