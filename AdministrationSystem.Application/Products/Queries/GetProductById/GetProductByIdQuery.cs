using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Products;

namespace AdministrationSystem.Application.Products.Queries.GetById;

public record GetProductByIdQuery(Guid ProductId) : IRequest<ErrorOr<Product>>;
