namespace AdministrationSystem.Contracts.WebSites;

public record UpdateWebSiteRequest(
    string Url,
    string Name,
    string Email
);
