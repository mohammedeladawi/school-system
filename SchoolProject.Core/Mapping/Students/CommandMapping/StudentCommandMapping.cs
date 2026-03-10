using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students;

public partial class StudentProfile
{
    private void MapAddStudentCommandToStudent()
    {
        CreateMap<AddStudentCommand, Student>();
    }
}