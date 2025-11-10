namespace AdministrationSystem.Contracts.WebSites;

public record CreateWebSiteRequest(
    string Url,
    string Name,
    string Email
);
