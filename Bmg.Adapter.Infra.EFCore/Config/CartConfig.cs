using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bmg.Domain;

namespace Bmg.Adapter.Infra.EFCore.Config;

public class CartConfig : IEntityTypeConfiguration<CartEntity>
{
    public void Configure(EntityTypeBuilder<CartEntity> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedNever();

        builder
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany<CartItemEntity>()
            .WithOne(ci => ci.Cart!)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.Payment)
            .WithOne(p => p.Cart!)
            .HasForeignKey<PaymentEntity>(p => p.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}