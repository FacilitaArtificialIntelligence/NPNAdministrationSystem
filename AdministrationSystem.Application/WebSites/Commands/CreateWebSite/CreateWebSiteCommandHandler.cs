using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.WebSites;
using AdministrationSystem.Application.Common.Interfaces;

namespace AdministrationSystem.Application.WebSites.Commands.CreateWebSite;

public class CreateWebSiteCommandHandler : IRequestHandler<CreateWebSiteCommand, ErrorOr<WebSite>>
{
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public CreateWebSiteCommandHandler(
        IWebSiteRepository webSiteRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _webSiteRepository = webSiteRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<WebSite>> Handle(CreateWebSiteCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var webSite = new WebSite(
            command.Url,
            command.Name,
            command.Email);

        await _webSiteRepository.AddWebSiteAsync(webSite);
        await _unitOfWork.CommitChangesAsync();

        return webSite;
    }
}
