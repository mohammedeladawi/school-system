using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Domain.Helpers;

namespace SchoolProject.Domain.Entities;

public class Instructor : ApplicationUser
{
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; } = null;
    public int? SupervisorId { get; set; }
    public Instructor? Supervisor { get; set; }
    public List<Instructor> Subordinates { get; set; } = new();
    public List<Subject> Subjects { get; set; } = new();
}