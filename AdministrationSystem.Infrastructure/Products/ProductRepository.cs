
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Products;
using AdministrationSystem.Infrastructure.Common.Persistance;

using Microsoft.EntityFrameworkCore;

namespace AdministrationSystem.Infrastructure.Products;

public class ProductRepository : IProductsRepository
{
    private readonly AdministrationSystemDBContext _context;

    public ProductRepository(AdministrationSystemDBContext context)
    {
        _context = context;
    }

    public async Task CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<List<Product>> GetProductsBySiteIdAsync(Guid siteId)
    {
        return await _context.Products.Where(p => p.SiteId == siteId).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
    }

    public Task UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }

    public Task DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }
}
