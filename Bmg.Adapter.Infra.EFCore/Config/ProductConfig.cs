using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bmg.Domain;

namespace Bmg.Adapter.Infra.EFCore.Config;

public class ProductConfig : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Stock).IsRequired();

        builder
            .HasMany<CartItemEntity>()
            .WithOne(ci => ci.Product!)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}