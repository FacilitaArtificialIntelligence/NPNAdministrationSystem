
namespace AdministrationSystem.Contracts.Sales;

public record UpdateSaleRequest(
    DateTime SaleDate,
    decimal Amount
);
