using ErrorOr;

using MediatR;

namespace AdministrationSystem.Application.Sites.Commands.UpdateSite;

public record UpdateSiteCommand(
    Guid SiteId,
    string Name,
    string SubDomain,
    string Email,
    string Description
) : IRequest<ErrorOr<Success>>;
