
namespace AdministrationSystem.Application.Sales.Commands.Create;

using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Sales;

public class CreateSaleCommandHandler
    : IRequestHandler<CreateSaleCommand, ErrorOr<Sale>>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public CreateSaleCommandHandler(
        ISalesRepository salesRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _salesRepository = salesRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<Sale>> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }
        
        var sale = new Sale(
            command.ProductId,
            command.SiteId,
            command.UserId,
            command.SaleDate,
            command.Amount
        );

        await _salesRepository.AddSaleAsync(sale);
        await _unitOfWork.CommitChangesAsync();

        return sale;
    }
}
