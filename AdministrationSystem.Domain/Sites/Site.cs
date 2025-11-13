namespace AdministrationSystem.Domain.Sites;

public class Site
{
    public Guid SiteId { get; set; }
    public Guid WebSiteId { get; set; }
    public string SubDomain { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public Site(
        Guid webSiteId,
        string name,
        string subDomain,
        string email,
        string description,
        Guid? siteId = null)
    {
        SiteId = siteId ?? Guid.NewGuid();
        WebSiteId = webSiteId;
        Name = name;
        SubDomain = subDomain;
        Email = email;
        Description = description;
    }

    private Site() { }
}
