using SchoolProject.Application.Features.Student.Commands.AddStudent;
using SchoolProject.Application.Features.Student.Commands.EditStudent;
using SchoolProject.Application.Features.Student.Queries.GetAllStudents;
using SchoolProject.Application.Features.Student.Queries.GetStudentById;
using SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;
using SchoolProject.Data.Entities;

namespace SchoolProject.Application.Mapping.Students;

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