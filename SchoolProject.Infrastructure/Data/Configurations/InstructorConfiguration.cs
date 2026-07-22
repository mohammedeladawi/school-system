using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Seeder;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.NameEn).IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.NameAr)
            .HasMaxLength(200);

        builder.Ignore(d => d.Name);

        builder.HasOne(i => i.Department)
            .WithMany(d => d.Instructors)
            .HasForeignKey(i => i.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(i => i.Supervisor)
            .WithMany(s => s.Subordinates)
            .HasForeignKey(i => i.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data
        builder.HasData(SeedData.Instructors);
    }
};
