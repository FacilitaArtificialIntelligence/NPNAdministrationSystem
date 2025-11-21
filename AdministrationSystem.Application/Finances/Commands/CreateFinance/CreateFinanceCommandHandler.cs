using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Finances;

namespace AdministrationSystem.Application.Finances.Commands.CreateFinance;


public class CreateFinanceCommandHandler
    : IRequestHandler<CreateFinanceCommand, ErrorOr<Finance>>
{
    private readonly IFinanceRepository _financeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public CreateFinanceCommandHandler(
        IFinanceRepository financeRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _financeRepository = financeRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Finance>> Handle(CreateFinanceCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        if (currentUser is null || currentUser.Role != 1)
        {
            return Error.Unauthorized("User.Unauthorized", "User is not authorized to perform this action.");
        }

        var finance = new Finance(
            command.UserId,
            command.SaleId,
            command.SiteId,
            command.TotalRevenue,
            command.Description,
            DateTime.Now);

        await _financeRepository.AddFinanceRecordAsync(finance);
        await _unitOfWork.CommitChangesAsync();

        return finance;
    }
}
