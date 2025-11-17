using AdministrationSystem.Domain.Sales;

namespace AdministrationSystem.Application.Common.Interfaces;

public interface ISalesRepository
{
    public Task AddSaleAsync(Sale sale);
    public Task<Sale?> GetSaleByIdAsync(Guid saleId);
    public Task<List<Sale>> GetAllSalesAsync(Guid siteId);
    public Task DeleteSaleAsync(Sale sale);
    public Task<List<Sale>> GetSalesByProductIdAsync(Guid productId);
}