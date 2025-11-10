using AdministrationSystem.Domain.Sites;

namespace AdministrationSystem.Domain.WebSites;

public class WebSite
{
    public Guid WebSiteId { get; set; }
    public string Url { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<Site> Sites { get; set; } = new();

    public WebSite(
        string url,
        string name,
        string email,
        Guid? webSiteId = null)
    {
        WebSiteId = webSiteId ?? Guid.NewGuid();
        Url = url;
        Name = name;
        Email = email;
    }

    private WebSite() { }
}
