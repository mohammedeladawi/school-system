using SchoolProject.Application.Features.Student.Commands.EditStudent;
using SchoolProject.Application.Features.Student.Queries.GetAllStudents;
using SchoolProject.Application.Features.Student.Queries.GetStudentById;
using SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;
using SchoolProject.Domain.Entities;
using SchoolProject.Application.Features.Authentication.Commands.Register;
using SchoolProject.Application.Features.Student.Commands.RegisterStudent;

namespace SchoolProject.Application.Mapping.Students;

public partial class StudentProfile
{
    private void MapAddStudentCommandToStudent()
    {
        CreateMap<RegisterStudentCommand, Student>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }

    private void MapEditStudentCommandToStudent()
    {
        CreateMap<EditStudentCommand, Student>();
    }
}