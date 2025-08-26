using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bmg.Domain;

namespace Bmg.Adapter.Infra.EFCore.Config;

public class CartItemConfig : IEntityTypeConfiguration<CartItemEntity>
{
    public void Configure(EntityTypeBuilder<CartItemEntity> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(ci => new { ci.CartId, ci.ProductId });

        builder.Property(ci => ci.CartId).IsRequired();
        builder.Property(ci => ci.ProductId).IsRequired();
        builder.Property(ci => ci.Quantity).IsRequired();

        builder
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.Items!)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}