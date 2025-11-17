
namespace AdministrationSystem.Contracts.Sales;

public record CreateSaleRequest(
    Guid ProductId,
    Guid SiteId,
    Guid UserId,
    DateTime SaleDate,
    decimal Amount
);
