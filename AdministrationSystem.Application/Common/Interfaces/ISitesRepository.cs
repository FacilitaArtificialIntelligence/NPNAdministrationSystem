using AdministrationSystem.Domain.Sites;

namespace AdministrationSystem.Application.Common.Interfaces;

public interface ISitesRepository
{
    public Task CreateSiteAsync(Site site);
    public Task<Site?> GetSiteByIdAsync(Guid siteId);
    public Task<List<Site>> GetAllSitesAsync();
    public Task<List<Site>> GetAllSitesAsync(Guid webSiteId);
    public Task DeleteSiteAsync(Site site);
    public Task UpdateSiteAsync(Site site);
}
