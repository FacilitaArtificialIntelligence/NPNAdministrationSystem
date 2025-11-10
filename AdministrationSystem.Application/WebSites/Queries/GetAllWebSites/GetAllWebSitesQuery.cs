using MediatR;
using AdministrationSystem.Domain.WebSites;

namespace AdministrationSystem.Application.WebSites.Queries.GetAllWebSites;

public record GetAllWebSitesQuery() : IRequest<List<WebSite>>;
