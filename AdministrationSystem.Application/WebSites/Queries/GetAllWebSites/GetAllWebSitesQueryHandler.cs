using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.WebSites;

using ErrorOr;

namespace AdministrationSystem.Application.WebSites.Queries.GetAllWebSites;

public class GetAllWebSitesQueryHandler : IRequestHandler<GetAllWebSitesQuery, ErrorOr<List<WebSite>>>
{
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GetAllWebSitesQueryHandler(
        IWebSiteRepository webSiteRepository,
        ICurrentUserProvider currentUserProvider)
    {
        _webSiteRepository = webSiteRepository;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<List<WebSite>>> Handle(GetAllWebSitesQuery query, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null)
        {
            throw new UnauthorizedAccessException("Current user is unauthorized.");
        }
        
        return await _webSiteRepository.GetAllWebSitesAsync();
    }
}
