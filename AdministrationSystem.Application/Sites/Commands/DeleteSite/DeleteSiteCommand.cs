using ErrorOr;
using MediatR;

namespace AdministrationSystem.Application.Sites.Commands.DeleteSite;

public record DeleteSiteCommand(Guid SiteId) : IRequest<ErrorOr<Deleted>>;
