namespace AdministrationSystem.Domain.Products;

public class Product
{
    public Guid ProductId { get; set; }
    public Guid SiteId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }

    public Product(
        Guid siteId,
        string name,
        string description,
        decimal price,
        Guid? productId = null)
    {
        ProductId = productId ?? Guid.NewGuid();
        SiteId = siteId;
        Name = name;
        Description = description;
        Price = price;
    }

    private Product() { }
}