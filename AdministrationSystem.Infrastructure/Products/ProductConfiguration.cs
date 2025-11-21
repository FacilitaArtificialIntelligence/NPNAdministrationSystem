using Microsoft.EntityFrameworkCore;
using AdministrationSystem.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdministrationSystem.Domain.Sites;

namespace AdministrationSystem.Infrastructure.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

        builder
            .HasOne<Site>()
            .WithMany()
            .HasForeignKey(x => x.SiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
