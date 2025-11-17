using ErrorOr;
using MediatR;

namespace AdministrationSystem.Application.Finances.Commands.DeleteFinance;

public record DeleteFinanceCommand(Guid FinanceId) : IRequest<ErrorOr<Deleted>>;
