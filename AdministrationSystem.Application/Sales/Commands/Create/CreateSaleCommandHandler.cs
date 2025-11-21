namespace AdministrationSystem.Application.Sales.Commands.Create;

using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Sales;
using AdministrationSystem.Domain.Finances;


public class CreateSaleCommandHandler
    : IRequestHandler<CreateSaleCommand, ErrorOr<Sale>>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IFinanceRepository _financeRepository;
    private readonly IProductsRepository _productsRepository;

    public CreateSaleCommandHandler(
        ISalesRepository salesRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider,
        IFinanceRepository financeRepository,
        IProductsRepository productsRepository)
    {
        _salesRepository = salesRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
        _financeRepository = financeRepository;
        _productsRepository = productsRepository;
    }

    public async Task<ErrorOr<Sale>> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var product = await _productsRepository.GetProductByIdAsync(command.ProductId);

        if (product is null)
        {
            return Error.NotFound(description: "Product not found");
        }
        
        var sale = new Sale(
            command.ProductId,
            command.SiteId,
            command.UserId,
            DateTime.Now,
            command.Amount
        );

        var finance = new Finance(
            command.UserId,
            sale.SaleId,
            command.SiteId,
            command.Amount * product.Price,
            $"Venda de {command.Amount} quantidades de {product.Name}",
            sale.SaleDate
        );

        await _salesRepository.AddSaleAsync(sale);
        await _financeRepository.AddFinanceRecordAsync(finance);
        await _unitOfWork.CommitChangesAsync();

        return sale;
    }
}
