using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Sales;
using ErrorOr;
using MediatR;

namespace AdministrationSystem.Application.Sales.Queries.GetByProduct;

public class GetSalesByProductQueryHandler
    : IRequestHandler<GetSalesByProductQuery, ErrorOr<List<Sale>>>
{
    private readonly ISalesRepository _salesRepository;

    public GetSalesByProductQueryHandler(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task<ErrorOr<List<Sale>>> Handle(GetSalesByProductQuery query, CancellationToken cancellationToken)
    {
        var sales = await _salesRepository.GetSalesByProductIdAsync(query.ProductId);
        return sales.ToList();
    }
}
