using System.Reflection;
using Microsoft.EntityFrameworkCore;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Users;

namespace AdministrationSystem.Infrastructure.Common.Persistance;
public class AdministrationSystemDBContext(
    DbContextOptions options) : DbContext(options), IUnitOfWork
{

    public DbSet<User> Users { get; set; } = null!;
    
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
