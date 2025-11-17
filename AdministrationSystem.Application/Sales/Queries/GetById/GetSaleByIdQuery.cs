using MediatR;
using ErrorOr;
using AdministrationSystem.Domain.Sales;

namespace AdministrationSystem.Application.Sales.Queries.GetById;

public record GetSaleByIdQuery(Guid SaleId) : IRequest<ErrorOr<Sale>>;
