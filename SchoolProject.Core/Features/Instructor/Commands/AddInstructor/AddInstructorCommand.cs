using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructor.Commands.AddInstructor;

public record AddInstructorCommand : IRequest<Response<string>>
{
    public string NameEn { get; set; } = null!;
    public string NameAr { get; set; } = null!;
    public int? DepartmentId { get; set; }
    public int? SupervisorId { get; set; }
    public IFormFile? Image { get; set; }
}