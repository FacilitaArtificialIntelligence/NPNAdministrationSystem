
namespace AdministrationSystem.Contracts.Sales;

public record SaleResponse(
    Guid SaleId,
    Guid ProductId,
    Guid SiteId,
    Guid UserId,
    DateTime SaleDate,
    decimal Amount
);
