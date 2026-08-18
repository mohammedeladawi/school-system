using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Seeder;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasOne(i => i.Department)
            .WithMany(d => d.Instructors)
            .HasForeignKey(i => i.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(i => i.Supervisor)
            .WithMany(s => s.Subordinates)
            .HasForeignKey(i => i.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed instructor data
        builder.HasData(SeedData.Instructors);
    }
};
