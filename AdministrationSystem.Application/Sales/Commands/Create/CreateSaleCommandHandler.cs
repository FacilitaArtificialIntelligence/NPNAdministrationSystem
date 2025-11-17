
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

    public CreateSaleCommandHandler(ISalesRepository salesRepository, IUnitOfWork unitOfWork)
    {
        _salesRepository = salesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Sale>> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
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
