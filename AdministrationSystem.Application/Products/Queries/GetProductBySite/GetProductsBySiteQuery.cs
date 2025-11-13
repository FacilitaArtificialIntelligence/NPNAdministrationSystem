using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Products;

namespace AdministrationSystem.Application.Products.Queries.GetBySite;

public record GetProductsBySiteQuery(Guid SiteId) : IRequest<ErrorOr<List<Product>>>;
