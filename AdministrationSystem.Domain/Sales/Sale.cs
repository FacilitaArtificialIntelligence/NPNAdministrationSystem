namespace AdministrationSystem.Domain.Sales;

public class Sale
{
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public Guid SiteId { get; set; }
    public Guid UserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal Amount { get; set; }

    public Sale(
        Guid productId,
        Guid siteId,
        Guid userId,
        DateTime saleDate,
        decimal amount,
        Guid? saleId = null)
    {
        SaleId = saleId ?? Guid.NewGuid();
        ProductId = productId;
        SiteId = siteId;
        UserId = userId;
        SaleDate = saleDate;
        Amount = amount;
    }

    private Sale() { }
}
