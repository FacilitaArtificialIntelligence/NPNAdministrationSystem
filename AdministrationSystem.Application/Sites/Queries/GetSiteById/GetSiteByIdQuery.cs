using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Sites;

namespace AdministrationSystem.Application.Sites.Queries.GetSiteById;

public record GetSiteByIdQuery(Guid SiteId) : IRequest<ErrorOr<Site>>;
