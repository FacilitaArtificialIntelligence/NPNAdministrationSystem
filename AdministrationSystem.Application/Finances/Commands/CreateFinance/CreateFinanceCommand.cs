using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Finances;

namespace AdministrationSystem.Application.Finances.Commands.CreateFinance;

public record CreateFinanceCommand(
    Guid SiteId,
    Guid UserId,
    Guid SaleId,
    decimal TotalRevenue,
    string Description
) : IRequest<ErrorOr<Finance>>;
