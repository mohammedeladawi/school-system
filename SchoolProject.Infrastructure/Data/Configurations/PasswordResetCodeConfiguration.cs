using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class PasswordResetCodeConfiguration : IEntityTypeConfiguration<PasswordResetCode>
{
    public void Configure(EntityTypeBuilder<PasswordResetCode> builder)
    {

        builder.HasKey(prc => prc.Id);

        builder.HasIndex(prc => new { prc.UserId, prc.IsRevoked });
        builder.HasIndex(prc => prc.HashedCode);

        builder.Property(prc => prc.HashedCode)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(prc => prc.User)
            .WithMany()
            .HasForeignKey(prc => prc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
};
