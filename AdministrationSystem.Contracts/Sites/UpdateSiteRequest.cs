namespace AdministrationSystem.Contracts.Sites;

public record UpdateSiteRequest(
    string Name,
    string SubDomain,
    string Email,
    string Description
);
