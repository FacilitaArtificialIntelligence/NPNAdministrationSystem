
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Finances;
using AdministrationSystem.Infrastructure.Common.Persistance;

using Microsoft.EntityFrameworkCore;

namespace AdministrationSystem.Infrastructure.Finances;

public class FinanceRepository : IFinanceRepository
{
    private readonly AdministrationSystemDBContext _context;

    public FinanceRepository(AdministrationSystemDBContext context)
    {
        _context = context;
    }

    public async Task AddFinanceRecordAsync(Finance finance)
    {
        await _context.Finances.AddAsync(finance);
    }

    public Task DeleteFinanceAsync(Finance finance)
    {
        _context.Finances.Remove(finance);
        return Task.CompletedTask;
    }

    public async Task<List<Finance>> GetAllFinancesBySubdomainAsync(Guid siteId)
    {
        return await _context.Finances
            .Where(f => f.SiteId == siteId)
            .ToListAsync();
    }

    public async Task<Finance?> GetFinanceByIdAsync(Guid financeId)
    {
        return await _context.Finances.FirstOrDefaultAsync(f => f.FinanceId == financeId);
    }
}
