using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Instructor.Commands.AddInstructor;

public record AddInstructorCommand : IRequest<Response<string>>
{
    public string NameEn { get; set; } = null!;
    public string NameAr { get; set; } = null!;
    public int? DepartmentId { get; set; }
    public int? SupervisorId { get; set; }
    public IFormFile? Image { get; set; }
}