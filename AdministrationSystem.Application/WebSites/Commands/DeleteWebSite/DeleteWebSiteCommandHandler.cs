using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;

namespace AdministrationSystem.Application.WebSites.Commands.DeleteWebSite;

public class DeleteWebSiteCommandHandler 
    : IRequestHandler<DeleteWebSiteCommand, ErrorOr<Deleted>>
{
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public DeleteWebSiteCommandHandler(
        IWebSiteRepository webSiteRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _webSiteRepository = webSiteRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteWebSiteCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var webSite = await _webSiteRepository.GetWebSiteByIdAsync(command.WebSiteId);

        if (webSite is null)
            return Error.NotFound("WebSite.NotFound", "Website not found");

        await _webSiteRepository.DeleteWebSiteAsync(webSite);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
