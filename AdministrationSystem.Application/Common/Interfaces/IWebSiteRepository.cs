using AdministrationSystem.Domain.WebSites;

namespace AdministrationSystem.Application.Common.Interfaces;

public interface IWebSiteRepository
{
    public Task<WebSite?> GetWebSiteByIdAsync(Guid webSiteId);
    public Task<List<WebSite>> GetAllWebSitesAsync();
    public Task AddWebSiteAsync(WebSite webSite);
    public Task UpdateWebSiteAsync(WebSite webSite);
    public Task DeleteWebSiteAsync(WebSite webSite);
}