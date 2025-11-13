    using ErrorOr;
using MediatR;

namespace AdministrationSystem.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest<ErrorOr<Deleted>>;
