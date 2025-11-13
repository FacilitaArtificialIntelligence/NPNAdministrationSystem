using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Products;

namespace AdministrationSystem.Application.Products.Queries.GetBySite;

public class GetProductsBySiteQueryHandler 
    : IRequestHandler<GetProductsBySiteQuery, ErrorOr<List<Product>>>
{
    private readonly IProductsRepository _productRepository;

    public GetProductsBySiteQueryHandler(IProductsRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<List<Product>>> Handle(GetProductsBySiteQuery q, CancellationToken ct)
    {
        var products = await _productRepository.GetProductsBySiteIdAsync(q.SiteId);
        return products;
    }
}
