
namespace AdministrationSystem.Contracts.Finance;

public record FinanceResponse(
    Guid FinanceId,
    Guid SubDomain,
    Guid UserId,
    Guid SaleId,
    decimal TotalRevenue,
    string Description
);
