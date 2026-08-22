using SchoolProject.Application.Features.Student.Commands.EditStudent;
using SchoolProject.Application.Features.Student.Commands.RegisterStudent;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Students;

public partial class StudentProfile
{
    private void MapRegisterStudentCommandToStudent()
    {
        CreateMap<RegisterStudentCommand, Student>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }

    private void MapEditStudentCommandToStudent()
    {
        CreateMap<EditStudentCommand, Student>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}