namespace AdministrationSystem.Application.Finances.Commands.DeleteFinance;

using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;

public class DeleteFinanceCommandHandler
    : IRequestHandler<DeleteFinanceCommand, ErrorOr<Deleted>>
{
    private readonly IFinanceRepository _financeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFinanceCommandHandler(IFinanceRepository financeRepository, IUnitOfWork unitOfWork)
    {
        _financeRepository = financeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteFinanceCommand command, CancellationToken cancellationToken)
    {
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
