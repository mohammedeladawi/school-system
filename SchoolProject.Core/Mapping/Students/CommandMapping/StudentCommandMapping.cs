using SchoolProject.Core.Features.Student.Commands.Models;
using SchoolProject.Core.Features.Student.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students;

public partial class StudentProfile
{
    private void MapAddStudentCommandToStudent()
    {
        CreateMap<AddStudentCommand, Student>();
    }

    private void MapEditStudentCommandToStudent()
    {
        CreateMap<EditStudentCommand, Student>();
    }
}