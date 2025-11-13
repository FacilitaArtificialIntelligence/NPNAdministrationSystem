
namespace AdministrationSystem.Contracts.Products;

public record ProductResponse(
    Guid ProductId,
    Guid SiteId,
    string Name,
    string Description,
    decimal Price
);
