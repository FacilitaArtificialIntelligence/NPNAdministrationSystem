using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Finances;

namespace AdministrationSystem.Application.Finances.Queries.GetById;

public class GetFinanceByIdQueryHandler
    : IRequestHandler<GetFinanceByIdQuery, ErrorOr<Finance>>
{
    private readonly IFinanceRepository _financeRepository;

    public GetFinanceByIdQueryHandler(IFinanceRepository financeRepository)
    {
        _financeRepository = financeRepository;
    }

    public async Task<ErrorOr<Finance>> Handle(GetFinanceByIdQuery query, CancellationToken cancellationToken)
    {
        var finance = await _financeRepository.GetFinanceByIdAsync(query.FinanceId);
        return finance is null
            ? Error.NotFound("Finance.NotFound", "Finance not found")
            : finance;
    }
}
