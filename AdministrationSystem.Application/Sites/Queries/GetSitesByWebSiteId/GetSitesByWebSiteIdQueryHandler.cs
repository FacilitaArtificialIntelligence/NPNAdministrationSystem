using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Sites;
using ErrorOr;

namespace AdministrationSystem.Application.Sites.Queries.GetSitesByWebSiteId;

public class GetSitesByWebSiteIdQueryHandler 
    : IRequestHandler<GetSitesByWebSiteIdQuery, ErrorOr<List<Site>>>
{
    private readonly ISitesRepository _sitesRepository;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GetSitesByWebSiteIdQueryHandler(
        ISitesRepository sitesRepository,
        ICurrentUserProvider currentUserProvider)
    {
        _sitesRepository = sitesRepository;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<List<Site>>> Handle(GetSitesByWebSiteIdQuery query, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var webSite = await _sitesRepository.GetAllSitesAsync(query.WebSiteId);

        if (webSite == null)
        {
            return Error.NotFound("Website not found");
        }

        return await _sitesRepository.GetAllSitesAsync(query.WebSiteId);
    }
}
