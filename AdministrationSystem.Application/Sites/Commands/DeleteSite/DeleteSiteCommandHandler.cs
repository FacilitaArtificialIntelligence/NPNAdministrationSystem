using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;

namespace AdministrationSystem.Application.Sites.Commands.DeleteSite;

public class DeleteSiteCommandHandler 
    : IRequestHandler<DeleteSiteCommand, ErrorOr<Deleted>>
{
    private readonly ISitesRepository _sitesRepository;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSiteCommandHandler(
        ISitesRepository sitesRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _sitesRepository = sitesRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSiteCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var site = await _sitesRepository.GetSiteByIdAsync(command.SiteId);

        if (site is null)
            return Error.NotFound("Site.NotFound", "Site not found");

        await _sitesRepository.DeleteSiteAsync(site);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
