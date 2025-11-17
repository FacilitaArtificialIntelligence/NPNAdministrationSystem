
namespace AdministrationSystem.Application.Sales.Queries.GetById;

using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Sales;


using ErrorOr;


using MediatR;

public class GetSaleByIdQueryHandler
    : IRequestHandler<GetSaleByIdQuery, ErrorOr<Sale>>
{
    private readonly ISalesRepository _saleRepository;

    public GetSaleByIdQueryHandler(ISalesRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<ErrorOr<Sale>> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetSaleByIdAsync(query.SaleId);
        return sale is null
            ? Error.NotFound("Sale.NotFound", "Sale not found")
            : sale;
    }
}
