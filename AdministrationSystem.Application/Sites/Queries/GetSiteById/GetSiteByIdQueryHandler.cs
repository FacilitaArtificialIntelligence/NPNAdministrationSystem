using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Sites;

namespace AdministrationSystem.Application.Sites.Queries.GetSiteById;

public class GetSiteByIdQueryHandler 
    : IRequestHandler<GetSiteByIdQuery, ErrorOr<Site>>
{
    private readonly ISitesRepository _sitesRepository;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GetSiteByIdQueryHandler(
        ISitesRepository sitesRepository,
        ICurrentUserProvider currentUserProvider)
    {
        _sitesRepository = sitesRepository;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Site>> Handle(GetSiteByIdQuery query, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        
        if (currentUser == null)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var site = await _sitesRepository.GetSiteByIdAsync(query.SiteId);

        return site is null ? 
            (ErrorOr<Site>)Error.NotFound("Site.NotFound", "Site not found") 
            : (ErrorOr<Site>)site;     
    }
}
