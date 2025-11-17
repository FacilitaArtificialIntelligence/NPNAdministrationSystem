
namespace AdministrationSystem.Contracts.Finance;

public record CreateFinanceRequest(
    Guid SubDomain,
    Guid UserId,
    Guid SaleId,
    decimal TotalRevenue,
    string Description
);
