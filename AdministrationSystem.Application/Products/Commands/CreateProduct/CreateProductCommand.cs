using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Products;

namespace AdministrationSystem.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    Guid SiteId,
    string Name,
    string Description,
    decimal Price
) : IRequest<ErrorOr<Product>>;
