using SchoolProject.Core.Features.Instructor.Commands.Models;

namespace SchoolProject.Core.Mapping.Instructor;

public partial class InstructorProfile
{
    private void MapAddInstructorCommandToInstructor()
    {
        CreateMap<AddInstructorCommand, Data.Entities.Instructor>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .ForSourceMember(src => src.Image, opt => opt.DoNotValidate());
    }
}