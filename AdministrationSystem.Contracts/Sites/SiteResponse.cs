namespace AdministrationSystem.Contracts.Sites;

public record SiteResponse(
    Guid SiteId,
    Guid WebSiteId,
    string Name,
    string SubDomain,
    string Email,
    string Description
);
