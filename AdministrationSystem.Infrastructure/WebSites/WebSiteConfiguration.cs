using Microsoft.EntityFrameworkCore;
using AdministrationSystem.Domain.WebSites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdministrationSystem.Infrastructure.WebSites;

public class WebSiteConfiguration : IEntityTypeConfiguration<WebSite>
{
    public void Configure(EntityTypeBuilder<WebSite> builder)
    {
        builder.ToTable("WebSites");

        builder.HasKey(x => x.WebSiteId);
        builder.Property(x => x.WebSiteId).ValueGeneratedNever();
        builder.Property(x => x.Url).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(250);

        builder
            .HasMany(x => x.Sites)
            .WithOne()
            .HasForeignKey(x => x.WebSiteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Sites).AutoInclude();
    }
}
