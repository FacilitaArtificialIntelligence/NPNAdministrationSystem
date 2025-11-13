using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Products;

namespace AdministrationSystem.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler 
    : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IProductsRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly ISitesRepository _sitesRepository;

    public CreateProductCommandHandler(
        IProductsRepository productRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider,
        ISitesRepository sitesRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
        _sitesRepository = sitesRepository;
    }

    public async Task<ErrorOr<Product>> Handle(CreateProductCommand command, CancellationToken ct)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        if (currentUser == null || currentUser.Role != 1)
        {
            return Error.Failure("Unauthorized", "Current user is unauthorized.");
        }

        var site = await _sitesRepository.GetSiteByIdAsync(command.SiteId);

        if (site == null)
        {
            return Error.NotFound(description: "Site not found.");
        }

        var product = new Product(
            command.SiteId,
            command.Name,
            command.Description,
            command.Price);

        await _productRepository.CreateProductAsync(product);
        await _unitOfWork.CommitChangesAsync();

        return product;
    }
}
