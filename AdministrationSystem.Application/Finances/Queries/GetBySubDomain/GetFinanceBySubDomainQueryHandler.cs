using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Finances;

namespace AdministrationSystem.Application.Finances.Queries.GetBySubDomain;

public class GetFinanceBySubDomainQueryHandler
    : IRequestHandler<GetFinanceBySubDomainQuery, ErrorOr<List<Finance>>>
{
    private readonly IFinanceRepository _financeRepository;

    public GetFinanceBySubDomainQueryHandler(IFinanceRepository financeRepository)
    {
        _financeRepository = financeRepository;
    }

    public async Task<ErrorOr<List<Finance>>> Handle(GetFinanceBySubDomainQuery query, CancellationToken cancellationToken)
    {
        var finances = await _financeRepository.GetAllFinancesBySubdomainAsync(query.SubDomain);
        return finances;
    }
}
