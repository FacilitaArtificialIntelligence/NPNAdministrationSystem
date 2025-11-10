using AdministrationSystem.Domain.Finances;

namespace AdministrationSystem.Application.Common.Interfaces;

public interface IFinanceRepository
{
    public Task AddFinanceRecordAsync(Finance finance);
    public Task<Finance?> GetFinanceByIdAsync(Guid financeId);
    public Task<List<Finance>> GetAllFinancesAsync();
    public Task DeleteFinanceAsync(Finance finance);
}