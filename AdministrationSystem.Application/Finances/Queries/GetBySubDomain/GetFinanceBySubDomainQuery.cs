using AdministrationSystem.Domain.Finances;
using ErrorOr;
using MediatR;

namespace AdministrationSystem.Application.Finances.Queries.GetBySubDomain;

public record GetFinanceBySubDomainQuery(Guid SubDomain)
    : IRequest<ErrorOr<List<Finance>>>;
