
namespace AdministrationSystem.Contracts.Sales;

public record CreateSaleRequest(
    Guid ProductId,
    Guid SiteId,
    Guid UserId,
    decimal Amount
);
