
namespace AdministrationSystem.Contracts.Sales;

public record SaleResponse(
    Guid SaleId,
    Guid ProductId,
    DateTime SaleDate,
    decimal Amount
);
