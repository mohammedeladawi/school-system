using SchoolProject.Shared.Helpers;

namespace SchoolProject.Data.Entities;

public class Instructor
{
    public int Id { get; set; }
    public string NameEn { get; set; } = null!;
    public string? NameAr { get; set; }
    public string Name => GeneralLocalizableEntity.LocalizeText(NameEn, NameAr);
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; } = null;
    public int? SupervisorId { get; set; }
    public Instructor? Supervisor { get; set; }

    public List<Instructor> Subordinates { get; set; } = new();
    public List<Subject> Subjects { get; set; } = new();
}