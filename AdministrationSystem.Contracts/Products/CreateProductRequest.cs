
namespace AdministrationSystem.Contracts.Products;

public record CreateProductRequest(
    Guid SiteId,
    string Name,
    string Description,
    decimal Price
);
