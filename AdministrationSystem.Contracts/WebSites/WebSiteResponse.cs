namespace AdministrationSystem.Contracts.WebSites;

public record WebSiteResponse(
    Guid WebSiteId,
    string Url,
    string Name,
    string Email
);
