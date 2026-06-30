using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Infrastructure.Seeder;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.HasData(SeedData.ApplicationUserRoles);
    }
}