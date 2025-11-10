using ErrorOr;
using MediatR;

namespace AdministrationSystem.Application.WebSites.Commands.DeleteWebSite;

public record DeleteWebSiteCommand(
    Guid WebSiteId
) : IRequest<ErrorOr<Deleted>>;
