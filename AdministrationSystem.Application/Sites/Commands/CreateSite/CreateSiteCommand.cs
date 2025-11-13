using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Sites;

namespace AdministrationSystem.Application.Sites.Commands.CreateSite;

public record CreateSiteCommand(
    Guid WebSiteId,
    string Name,
    string SubDomain,
    string Email,
    string Description
) : IRequest<ErrorOr<Site>>;
