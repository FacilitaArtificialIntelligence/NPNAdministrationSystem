using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Models;

namespace AdministrationSystem.Application.WebSites.Commands.UpdateWebSite;

public record UpdateWebSiteCommand(
    Guid WebSiteId,
    string Url,
    string Name,
    string Email
) : IRequest<ErrorOr<Success>>;
