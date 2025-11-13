using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Application.Common.Models;

namespace AdministrationSystem.Application.Sites.Commands.UpdateSite;

public class UpdateSiteCommandHandler 
    : IRequestHandler<UpdateSiteCommand, ErrorOr<Success>>
{
    private readonly ISitesRepository _sitesRepository;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSiteCommandHandler(
        ISitesRepository sitesRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _sitesRepository = sitesRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateSiteCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var site = await _sitesRepository.GetSiteByIdAsync(command.SiteId);

        if (site is null)
            return Error.NotFound("Site.NotFound", "Site not found");

        site.Name = command.Name;
        site.SubDomain = command.SubDomain;
        site.Email = command.Email;
        site.Description = command.Description;

        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}
