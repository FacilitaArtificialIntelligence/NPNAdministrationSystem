using System.Reflection;
using Microsoft.EntityFrameworkCore;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Users;
using AdministrationSystem.Domain.Finances;
using AdministrationSystem.Domain.Products;
using AdministrationSystem.Domain.Sales;
using AdministrationSystem.Domain.Sites;
using AdministrationSystem.Domain.WebSites;


namespace AdministrationSystem.Infrastructure.Common.Persistance;
public class AdministrationSystemDBContext(
    DbContextOptions options) : DbContext(options), IUnitOfWork
{

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<WebSite> WebSites { get; set; } = null!;
    public DbSet<Site> Sites { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<Finance> Finances { get; set; } = null!;
    
    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
