using AdministrationSystem.Domain.Products;

namespace AdministrationSystem.Application.Common.Interfaces;

public interface IProductsRepository
{
    Task CreateProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(Guid productId);
    Task<List<Product>> GetProductsBySiteIdAsync(Guid siteId);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Product product);
}