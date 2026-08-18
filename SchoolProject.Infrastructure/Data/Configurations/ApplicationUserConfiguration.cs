using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities.Identities;
using SchoolProject.Infrastructure.Seeder;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
       public void Configure(EntityTypeBuilder<ApplicationUser> builder)
       {
              builder.Property(au => au.Email)
                     .IsRequired();

              builder.Property(au => au.UserName)
                     .IsRequired();

              builder.Property(au => au.PasswordHash)
                     .IsRequired();

              builder.Ignore(au => au.Name);

              builder.Property(au => au.NameEn)
                             .IsRequired()
                             .HasMaxLength(200);

              builder.Property(au => au.NameAr)
                     .HasMaxLength(200);

              builder.Property(au => au.UserType)
                     .HasConversion<string>();

              builder.UseTptMappingStrategy();

              builder.HasData(SeedData.ApplicationUsers);
       }
};
