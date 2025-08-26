using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bmg.Domain;

namespace Bmg.Adapter.Infra.EFCore.Config;

public class PaymentConfig : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Property(p => p.CartId).IsRequired();
        builder.Property(p => p.Amount).IsRequired();

        builder
            .Property(p => p.Status)
            .HasConversion<string>()
            .IsRequired();
        builder
            .Property(p => p.Method)
            .HasConversion<string>()
            .IsRequired();
               
        builder.HasOne(p => p.Cart)
            .WithOne(c => c.Payment!)
            .HasForeignKey<PaymentEntity>(p => p.CartId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}