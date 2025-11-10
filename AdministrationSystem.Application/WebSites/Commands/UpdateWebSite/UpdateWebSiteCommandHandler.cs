using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Application.Common.Models;

namespace AdministrationSystem.Application.WebSites.Commands.UpdateWebSite;

public class UpdateWebSiteCommandHandler 
    : IRequestHandler<UpdateWebSiteCommand, ErrorOr<Success>>
{
    private readonly IWebSiteRepository _webSiteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public UpdateWebSiteCommandHandler(
        IWebSiteRepository webSiteRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _webSiteRepository = webSiteRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateWebSiteCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }
        
        var webSite = await _webSiteRepository.GetWebSiteByIdAsync(command.WebSiteId);
        if (webSite is null)
            return Error.NotFound("WebSite.NotFound", "Website not found");

        webSite.Url = command.Url;
        webSite.Name = command.Name;
        webSite.Email = command.Email;

        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}
