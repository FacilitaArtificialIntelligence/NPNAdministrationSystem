namespace AdministrationSystem.Application.Finances.Queries.GetById;

using ErrorOr;
using MediatR;
using AdministrationSystem.Domain.Finances;


public record GetFinanceByIdQuery(Guid FinanceId) : IRequest<ErrorOr<Finance>>;
