using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;

namespace AdministrationSystem.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler 
    : IRequestHandler<DeleteProductCommand, ErrorOr<Deleted>>
{
    private readonly IProductsRepository _productRepository;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(
        IProductsRepository productRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _currentUserProvider = currentUserProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteProductCommand command, CancellationToken ct)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var product = await _productRepository.GetProductByIdAsync(command.ProductId);

        if (product is null)
            return Error.NotFound("Product.NotFound", "Product not found");

        await _productRepository.DeleteProduct(product);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
