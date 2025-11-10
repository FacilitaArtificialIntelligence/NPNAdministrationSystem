using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Sites;
using AdministrationSystem.Infrastructure.Common.Persistance;

using Microsoft.EntityFrameworkCore;

namespace AdministrationSystem.Infrastructure.Sites;

public class SitesRepository : ISitesRepository
{
    private readonly AdministrationSystemDBContext _context;

    public SitesRepository(AdministrationSystemDBContext context)
    {
        _context = context;
    }

    public async Task AddSiteAsync(Site site)
    {
        await _context.Sites.AddAsync(site);
    }

    public Task DeleteSiteAsync(Site site)
    {
        _context.Sites.Remove(site);
        return Task.CompletedTask;
    }

    public async Task<List<Site>> GetAllSitesAsync()
    {
        return await _context.Sites.ToListAsync();
    }

    public Task<Site?> GetSiteByIdAsync(Guid siteId)
    {
        return _context.Sites.FirstOrDefaultAsync(s => s.SiteId == siteId);
    }

    public Task UpdateSiteAsync(Site site)
    {
        _context.Sites.Update(site);
        return Task.CompletedTask;
    }
}
