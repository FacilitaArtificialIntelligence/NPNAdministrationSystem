using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Sites;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Application.Common.Models;

namespace AdministrationSystem.Application.Sites.Commands.CreateSite;

public class CreateSiteCommandHandler 
    : IRequestHandler<CreateSiteCommand, ErrorOr<Site>>
{
    private readonly ISitesRepository _sitesRepository;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSiteCommandHandler(
        ISitesRepository sitesRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider,
        IWebSiteRepository webSiteRepository)
    {
        _sitesRepository = sitesRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
        _webSiteRepository = webSiteRepository;
    }

    public async Task<ErrorOr<Site>> Handle(CreateSiteCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var webSite = await _webSiteRepository.GetWebSiteByIdAsync(command.WebSiteId);

        if (webSite == null)
        {
            return Error.NotFound("WebSite not found.");
        }

        var site = new Site(
            webSite.WebSiteId,
            command.Name,
            command.SubDomain,
            command.Email,
            command.Description
        );

        await _sitesRepository.CreateSiteAsync(site);
        await _unitOfWork.CommitChangesAsync();

        return site;
    }
}
