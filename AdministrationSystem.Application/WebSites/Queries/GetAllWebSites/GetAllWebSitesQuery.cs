using MediatR;
using AdministrationSystem.Domain.WebSites;

using ErrorOr;

namespace AdministrationSystem.Application.WebSites.Queries.GetAllWebSites;

public record GetAllWebSitesQuery() : IRequest<ErrorOr<List<WebSite>>>;
