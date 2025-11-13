
namespace AdministrationSystem.Contracts.Products;

public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price
);
