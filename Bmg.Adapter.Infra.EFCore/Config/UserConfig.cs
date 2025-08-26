using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bmg.Domain;

namespace Bmg.Adapter.Infra.EFCore.Config;

public class UserConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(60);

        builder
            .Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder.HasMany(u => u.Carts)
            .WithOne(c => c.User!)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}