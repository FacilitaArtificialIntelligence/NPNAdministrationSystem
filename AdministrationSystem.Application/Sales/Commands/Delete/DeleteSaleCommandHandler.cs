
namespace AdministrationSystem.Application.Sales.Commands.Delete;

using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;

public class DeleteSaleCommandHandler
    : IRequestHandler<DeleteSaleCommand, ErrorOr<Deleted>>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public DeleteSaleCommandHandler(
        ISalesRepository salesRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _salesRepository = salesRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }
        
        var sale = await _salesRepository.GetSaleByIdAsync(command.SaleId);
        if (sale is null)
        {
            return Error.NotFound("Sale.NotFound", "Sale not found");
        }

        await _salesRepository.DeleteSaleAsync(sale);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
