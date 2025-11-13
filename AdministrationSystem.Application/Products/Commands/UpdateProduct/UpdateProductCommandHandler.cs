
using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;

namespace AdministrationSystem.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler 
    : IRequestHandler<UpdateProductCommand, ErrorOr<Success>>
{
    private readonly IProductsRepository _productRepository;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(
        IProductsRepository productRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _currentUserProvider = currentUserProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateProductCommand command, CancellationToken ct)
    {
        var product = await _productRepository.GetProductByIdAsync(command.ProductId);
        if (product is null)
            return Error.NotFound("Product.NotFound", "Product not found");

        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;

        await _productRepository.UpdateProduct(product);
        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}
