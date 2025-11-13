using MediatR;
using AdministrationSystem.Domain.Sites;
using ErrorOr;

namespace AdministrationSystem.Application.Sites.Queries.GetSitesByWebSiteId;

public record GetSitesByWebSiteIdQuery(Guid WebSiteId) : IRequest<ErrorOr<List<Site>>>;
