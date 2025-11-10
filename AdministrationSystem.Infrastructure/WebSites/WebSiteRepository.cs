
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.WebSites;
using AdministrationSystem.Infrastructure.Common.Persistance;

using Microsoft.EntityFrameworkCore;

namespace AdministrationSystem.Infrastructure.WebSites;

public class WebSiteRepository : IWebSiteRepository
{
    private readonly AdministrationSystemDBContext _context;

    public WebSiteRepository(AdministrationSystemDBContext context)
    {
        _context = context;
    }

    public async Task AddWebSiteAsync(WebSite webSite)
    {
        await _context.WebSites.AddAsync(webSite);
    }

    public Task DeleteWebSiteAsync(WebSite webSite)
    {
        _context.WebSites.Remove(webSite);
        return Task.CompletedTask;
    }

    public async Task<List<WebSite>> GetAllWebSitesAsync()
    {
        return await _context.WebSites.ToListAsync();
    }

    public async Task<WebSite?> GetWebSiteByIdAsync(Guid webSiteId)
    {
        return await _context.WebSites.FirstOrDefaultAsync(w => w.WebSiteId == webSiteId);
    }

    public Task UpdateWebSiteAsync(WebSite webSite)
    {
        _context.WebSites.Update(webSite);
        return Task.CompletedTask;
    }
}
