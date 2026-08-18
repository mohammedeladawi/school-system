using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Domain.Helpers;

namespace SchoolProject.Domain.Entities;

public class Student : ApplicationUser
{
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public List<Subject> Subjects { get; set; } = new();
}