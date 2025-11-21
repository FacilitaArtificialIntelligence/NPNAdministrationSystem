using AdministrationSystem.Domain.Products;
using AdministrationSystem.Domain.Sales;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdministrationSystem.Infrastructure.Sales;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(x => x.SaleId);
        builder.Property(x => x.SaleId).ValueGeneratedNever();
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.SaleDate).IsRequired();
        builder.Property(x => x.SiteId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
