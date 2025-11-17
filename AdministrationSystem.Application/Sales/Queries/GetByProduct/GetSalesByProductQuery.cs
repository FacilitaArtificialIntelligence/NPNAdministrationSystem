using MediatR;
using ErrorOr;
using AdministrationSystem.Domain.Sales;

namespace AdministrationSystem.Application.Sales.Queries.GetByProduct;

public record GetSalesByProductQuery(Guid ProductId)
    : IRequest<ErrorOr<List<Sale>>>;
