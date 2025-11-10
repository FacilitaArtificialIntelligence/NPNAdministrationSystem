using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.WebSites;

namespace AdministrationSystem.Application.WebSites.Commands.CreateWebSite;

public record CreateWebSiteCommand(
    string Url,
    string Name,
    string Email
) : IRequest<ErrorOr<WebSite>>;
