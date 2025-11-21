using Microsoft.EntityFrameworkCore;
using AdministrationSystem.Domain.Finances;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdministrationSystem.Domain.Sales;
using AdministrationSystem.Domain.Sites;

namespace AdministrationSystem.Infrastructure.Finances;

public class FinanceConfiguration : IEntityTypeConfiguration<Finance>
{
    public void Configure(EntityTypeBuilder<Finance> builder)
    {
        builder.ToTable("Finances");

        builder.HasKey(x => x.FinanceId);

        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.TotalRevenue).IsRequired().HasColumnType("decimal(18,2)");

        builder
            .HasOne<Sale>()
            .WithMany()
            .HasForeignKey(x => x.SaleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Site>()
            .WithMany()
            .HasForeignKey(x => x.SiteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
