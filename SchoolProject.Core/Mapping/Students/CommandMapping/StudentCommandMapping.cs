using SchoolProject.Core.Features.Student.Commands.AddStudent;
using SchoolProject.Core.Features.Student.Commands.EditStudent;
using SchoolProject.Core.Features.Student.Queries.GetAllStudents;
using SchoolProject.Core.Features.Student.Queries.GetStudentById;
using SchoolProject.Core.Features.Student.Queries.GetPaginatedStudents;
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