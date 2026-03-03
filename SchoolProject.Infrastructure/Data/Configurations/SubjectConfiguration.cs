using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(s => s.Students)
            .WithMany(s => s.Subjects)
            .UsingEntity<StudentSubject>(
                l => l.HasOne(ss => ss.Student).WithMany().HasForeignKey(ss => ss.StudentId),
                r => r.HasOne(ss => ss.Subject).WithMany().HasForeignKey(ss => ss.SubjectId),
                j => j.ToTable("StudentSubjects"));

        builder.HasMany(s => s.Departments)
            .WithMany(d => d.Subjects)
            .UsingEntity<DepartmentSubject>(
                l => l.HasOne(ds => ds.Department).WithMany().HasForeignKey(ds => ds.DepartmentId),
                r => r.HasOne(ds => ds.Subject).WithMany().HasForeignKey(ds => ds.SubjectId),
                j => j.ToTable("DepartmentSubjects"));

        // Seed data for Subjects
        builder.HasData(
            new Subject { Id = 1, Name = "Data Structures", Period = new DateTime(2026, 03, 03) },
            new Subject { Id = 2, Name = "Algorithms", Period = new DateTime(2026, 03, 03) },
            new Subject { Id = 3, Name = "Web Development", Period = new DateTime(2026, 03, 03) },
            new Subject { Id = 4, Name = "Calculus I", Period = new DateTime(2026, 03, 03) },
            new Subject { Id = 5, Name = "Linear Algebra", Period = new DateTime(2026, 03, 03) },
            new Subject { Id = 6, Name = "Mechanics", Period = new DateTime(2026, 03, 03) },
            new Subject { Id = 7, Name = "Electromagnetism", Period = new DateTime(2026, 03, 03) },
            new Subject { Id = 8, Name = "Organic Chemistry", Period = new DateTime(2026, 03, 03) },
            new Subject { Id = 9, Name = "Genetics", Period = new DateTime(2026, 03, 03) }
        );

        // Seed data for DepartmentSubject relationships
        builder.HasMany(s => s.Departments)
            .WithMany(d => d.Subjects)
            .UsingEntity<DepartmentSubject>(
                build => build.HasData(
                    // Computer Science department (1) - Subjects 1, 2, 3
                    new DepartmentSubject { Id = 1, DepartmentId = 1, SubjectId = 1 },
                    new DepartmentSubject { Id = 2, DepartmentId = 1, SubjectId = 2 },
                    new DepartmentSubject { Id = 3, DepartmentId = 1, SubjectId = 3 },
                    // Mathematics department (2) - Subjects 4, 5
                    new DepartmentSubject { Id = 4, DepartmentId = 2, SubjectId = 4 },
                    new DepartmentSubject { Id = 5, DepartmentId = 2, SubjectId = 5 },
                    // Physics department (3) - Subjects 6, 7
                    new DepartmentSubject { Id = 6, DepartmentId = 3, SubjectId = 6 },
                    new DepartmentSubject { Id = 7, DepartmentId = 3, SubjectId = 7 },
                    // Chemistry department (4) - Subject 8
                    new DepartmentSubject { Id = 8, DepartmentId = 4, SubjectId = 8 },
                    // Biology department (5) - Subject 9
                    new DepartmentSubject { Id = 9, DepartmentId = 5, SubjectId = 9 }
                ));
    }
}