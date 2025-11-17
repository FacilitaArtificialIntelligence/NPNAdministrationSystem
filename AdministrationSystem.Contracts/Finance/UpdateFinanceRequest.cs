
namespace AdministrationSystem.Contracts.Finance;

public record UpdateFinanceRequest(
    decimal TotalRevenue,
    string Description
);
