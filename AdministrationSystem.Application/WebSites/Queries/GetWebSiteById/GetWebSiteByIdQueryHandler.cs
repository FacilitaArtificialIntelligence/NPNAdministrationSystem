using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.WebSites;
using AdministrationSystem.Application.Common.Interfaces;

namespace AdministrationSystem.Application.WebSites.Queries.GetWebSiteById;

public class GetWebSiteByIdQueryHandler : IRequestHandler<GetWebSiteByIdQuery, ErrorOr<WebSite>>
{
    private readonly IWebSiteRepository _webSiteRepository;

    public GetWebSiteByIdQueryHandler(
        IWebSiteRepository webSiteRepository)
    {
        _webSiteRepository = webSiteRepository;
    }

    public async Task<ErrorOr<WebSite>> Handle(GetWebSiteByIdQuery query, CancellationToken cancellationToken)
    {
        var webSite = await _webSiteRepository.GetWebSiteByIdAsync(query.WebSiteId);

        return webSite is null ? 
            (ErrorOr<WebSite>)Error.NotFound("WebSite.NotFound", "Website not found") 
            : (ErrorOr<WebSite>)webSite;
    }
}
