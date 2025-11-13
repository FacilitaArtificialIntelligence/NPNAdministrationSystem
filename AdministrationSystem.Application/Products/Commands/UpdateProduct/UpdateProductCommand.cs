using ErrorOr;


using MediatR;

namespace AdministrationSystem.Application.Products.Commands.UpdateProduct;
public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price
) : IRequest<ErrorOr<Success>>;
