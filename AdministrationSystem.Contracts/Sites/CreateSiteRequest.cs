namespace AdministrationSystem.Contracts.Sites;

public record CreateSiteRequest(
    Guid WebSiteId,
    string Name,
    string SubDomain,
    string Email,
    string Description
);
