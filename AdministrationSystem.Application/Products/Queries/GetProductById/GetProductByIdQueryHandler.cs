
using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Products;

namespace AdministrationSystem.Application.Products.Queries.GetById;

public class GetProductByIdQueryHandler 
    : IRequestHandler<GetProductByIdQuery, ErrorOr<Product>>
{
    private readonly IProductsRepository _productRepository;

    public GetProductByIdQueryHandler(IProductsRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(GetProductByIdQuery q, CancellationToken ct)
    {
        var product = await _productRepository.GetProductByIdAsync(q.ProductId);

        return product is null
            ? Error.NotFound("Product.NotFound", "Product not found")
            : product;
    }
}
