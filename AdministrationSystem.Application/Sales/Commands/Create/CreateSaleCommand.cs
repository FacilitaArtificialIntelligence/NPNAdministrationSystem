using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Sales;

namespace AdministrationSystem.Application.Sales.Commands.Create;

public record CreateSaleCommand(
    Guid ProductId,
    Guid SiteId,
    Guid UserId,
    DateTime SaleDate,
    decimal Amount
) : IRequest<ErrorOr<Sale>>;
