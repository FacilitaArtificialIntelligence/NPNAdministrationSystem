using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.WebSites;
using AdministrationSystem.Application.Common.Interfaces;

namespace AdministrationSystem.Application.WebSites.Queries.GetWebSiteById;

public class GetWebSiteByIdQueryHandler : IRequestHandler<GetWebSiteByIdQuery, ErrorOr<WebSite>>
{
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GetWebSiteByIdQueryHandler(
        IWebSiteRepository webSiteRepository,
        ICurrentUserProvider currentUserProvider)
    {
        _webSiteRepository = webSiteRepository;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<WebSite>> Handle(GetWebSiteByIdQuery query, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        
        if (currentUser == null)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var webSite = await _webSiteRepository.GetWebSiteByIdAsync(query.WebSiteId);

        return webSite is null ? 
            (ErrorOr<WebSite>)Error.NotFound("WebSite.NotFound", "Website not found") 
            : (ErrorOr<WebSite>)webSite;
    }
}
