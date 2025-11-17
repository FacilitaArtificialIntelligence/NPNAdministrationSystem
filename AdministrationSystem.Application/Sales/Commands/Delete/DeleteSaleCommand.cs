
namespace AdministrationSystem.Application.Sales.Commands.Delete;

using ErrorOr;
using MediatR;

public record DeleteSaleCommand(Guid SaleId) : IRequest<ErrorOr<Deleted>>;
