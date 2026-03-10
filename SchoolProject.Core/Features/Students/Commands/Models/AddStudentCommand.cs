using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Response<string>>
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int? DepartmentId { get; set; }
    }
}