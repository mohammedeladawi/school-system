using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public record AddStudentCommand : IRequest<Response<string>>
    {
        public string NameEn { get; init; } = null!;
        public string NameAr { get; init; } = null!;
        public string Phone { get; init; } = null!;
        public string Address { get; init; } = null!;
        public int? DepartmentId { get; init; }
    }
}