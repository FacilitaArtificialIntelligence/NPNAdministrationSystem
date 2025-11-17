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

    public CreateFinanceCommandHandler(IFinanceRepository financeRepository, IUnitOfWork unitOfWork)
    {
        _financeRepository = financeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Finance>> Handle(CreateFinanceCommand command, CancellationToken cancellationToken)
    {
        var finance = new Finance(
            command.UserId,
            command.SaleId,
            command.SiteId,
            command.TotalRevenue,
            command.Description);

        await _financeRepository.AddFinanceRecordAsync(finance);
        await _unitOfWork.CommitChangesAsync();

        return finance;
    }
}
