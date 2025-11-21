namespace AdministrationSystem.Domain.Finances;

public class Finance
{
    public Guid FinanceId { get; set; }
    public Guid UserId { get; set; }
    public Guid SaleId { get; set; }
    public Guid SiteId { get; set; }
    public decimal TotalRevenue { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }

    public Finance(
        Guid userId,
        Guid saleId,
        Guid siteId,
        decimal totalRevenue,
        string description,
        DateTime date,
        Guid? financeId = null)
    {
        FinanceId = financeId ?? Guid.NewGuid();
        UserId = userId;
        SaleId = saleId;
        SiteId = siteId;
        TotalRevenue = totalRevenue;
        Description = description;
        Date = date;
    }

    private Finance() { }
}
