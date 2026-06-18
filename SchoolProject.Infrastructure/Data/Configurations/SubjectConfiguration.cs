using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.NameEn)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.NameAr)
            .HasMaxLength(200);

        builder.Ignore(s => s.Name);

        builder.HasMany(s => s.Students)
            .WithMany(s => s.Subjects)
            .UsingEntity<StudentSubject>(
                l => l.HasOne(ss => ss.Student).WithMany().HasForeignKey(ss => ss.StudentId),
                r => r.HasOne(ss => ss.Subject).WithMany().HasForeignKey(ss => ss.SubjectId),
                j =>
                {
                    j.HasKey(ss => new { ss.StudentId, ss.SubjectId });
                    j.ToTable("StudentSubjects");
                    j.Property(ss => ss.Grade)
                        .HasPrecision(5, 2);

                    j.HasData(SeedData.StudentSubjects);
                });

        builder.HasMany(s => s.Departments)
            .WithMany(d => d.Subjects)
            .UsingEntity<DepartmentSubject>(
                l => l.HasOne(ds => ds.Department).WithMany().HasForeignKey(ds => ds.DepartmentId),
                r => r.HasOne(ds => ds.Subject).WithMany().HasForeignKey(ds => ds.SubjectId),
                j =>
                {
                    j.HasKey(ds => new { ds.DepartmentId, ds.SubjectId });
                    j.ToTable("DepartmentSubjects");

                    j.HasData(SeedData.DepartmentSubjects);
                });

        builder.HasMany(s => s.Instructors)
            .WithMany(d => d.Subjects)
            .UsingEntity<InstructorSubject>(
                l => l.HasOne(ds => ds.Instructor).WithMany().HasForeignKey(ds => ds.InstructorId),
                r => r.HasOne(ds => ds.Subject).WithMany().HasForeignKey(ds => ds.SubjectId),
                j =>
                {
                    j.HasKey(ds => new { ds.InstructorId, ds.SubjectId });
                    j.ToTable("InstructorSubjects");

                    j.HasData(SeedData.InstructorSubjects);
                });


        // Seed data for Subjects
        builder.HasData(SeedData.Subjects);
    }
}