using AdministrationSystem.Domain.Products;

namespace AdministrationSystem.Application.Common.Interfaces;

public interface IProductsRepository
{
    public Task AddProductAsync(Product product);
    public Task<Product?> GetProductByIdAsync(Guid productId);
    public Task<List<Product>> GetAllProductsAsync(Guid siteId);
    public Task DeleteProductAsync(Product product);
    public Task UpdateProductAsync(Product product);
}
