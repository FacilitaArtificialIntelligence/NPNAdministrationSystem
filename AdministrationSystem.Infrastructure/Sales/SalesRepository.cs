
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Sales;
using AdministrationSystem.Infrastructure.Common.Persistance;

using Microsoft.EntityFrameworkCore;

namespace AdministrationSystem.Infrastructure.Sales;

public class SalesRepository : ISalesRepository
{
    private readonly AdministrationSystemDBContext _context;

    public SalesRepository(AdministrationSystemDBContext context)
    {
        _context = context;
    }

    public async Task AddSaleAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
    }

    public Task DeleteSaleAsync(Sale sale)
    {
        _context.Sales.Remove(sale);
        return Task.CompletedTask;
    }

    public async Task<List<Sale>> GetAllSalesAsync(Guid siteId)
    {
        return await _context.Sales.Where(s => s.SiteId == siteId).ToListAsync();
    }

    public async Task<Sale?> GetSaleByIdAsync(Guid saleId)
    {
        return await _context.Sales.FirstOrDefaultAsync(s => s.SaleId == saleId);
    }
}