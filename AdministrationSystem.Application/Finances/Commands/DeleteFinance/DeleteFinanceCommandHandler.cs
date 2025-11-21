namespace AdministrationSystem.Application.Finances.Commands.DeleteFinance;

using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;

public class DeleteFinanceCommandHandler
    : IRequestHandler<DeleteFinanceCommand, ErrorOr<Deleted>>
{
    private readonly IFinanceRepository _financeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public DeleteFinanceCommandHandler(
        IFinanceRepository financeRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _financeRepository = financeRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteFinanceCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var finance = await _financeRepository.GetFinanceByIdAsync(command.FinanceId);
        if (finance is null)
        {
            return Error.NotFound("Finance.NotFound", "Finance not found");
        }

        await _financeRepository.DeleteFinanceAsync(finance);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
