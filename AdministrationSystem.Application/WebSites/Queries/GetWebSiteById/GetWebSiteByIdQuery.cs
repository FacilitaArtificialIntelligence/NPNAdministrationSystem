using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.WebSites;

namespace AdministrationSystem.Application.WebSites.Queries.GetWebSiteById;

public record GetWebSiteByIdQuery(
    Guid WebSiteId
) : IRequest<ErrorOr<WebSite>>;
