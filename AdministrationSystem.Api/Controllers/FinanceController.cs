using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdministrationSystem.Contracts.Finance;
using AdministrationSystem.Application.Finances.Commands.CreateFinance;
using AdministrationSystem.Application.Finances.Commands.DeleteFinance;
using AdministrationSystem.Application.Finances.Queries.GetById;
using AdministrationSystem.Application.Finances.Queries.GetBySubDomain;

namespace AdministrationSystem.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class FinanceController : ApiController
{
    private readonly ISender _mediator;

    public FinanceController(ISender mediator)
    {
        _mediator = mediator;
    }

    // [HttpPost]
    // public async Task<IActionResult> Create(CreateFinanceRequest request)
    // {
    //     var command = new CreateFinanceCommand(
    //         request.SubDomain,
    //         request.UserId,
    //         request.SaleId,
    //         request.TotalRevenue,
    //         request.Description);

    //     var result = await _mediator.Send(command);

    //     return result.Match(
    //         finance => CreatedAtAction(
    //             nameof(GetFinanceById),
    //             new { financeId = finance.FinanceId },
    //             new FinanceResponse(
    //                 finance.FinanceId,
    //                 finance.SiteId,
    //                 finance.UserId,
    //                 finance.SaleId,
    //                 finance.TotalRevenue,
    //                 finance.Description
    //             )),
    //         Problem);
    // }

    // [HttpDelete("{financeId:guid}")]
    // public async Task<IActionResult> Delete(Guid financeId)
    // {
    //     var result = await _mediator.Send(new DeleteFinanceCommand(financeId));
    //     return result.Match(_ => NoContent(), Problem);
    // }

    [HttpGet("{financeId:guid}")]
    public async Task<IActionResult> GetFinanceById(Guid financeId)
    {
        var result = await _mediator.Send(new GetFinanceByIdQuery(financeId));

        return result.Match(
            f => Ok(new FinanceResponse(
                f.FinanceId,
                f.SiteId,
                f.UserId,
                f.SaleId,
                f.TotalRevenue,
                f.Description)),
            Problem);
    }

    [HttpGet("BySubDomain/{subDomain:guid}")]
    public async Task<IActionResult> GetBySubDomain(Guid subDomain)
    {
        var result = await _mediator.Send(new GetFinanceBySubDomainQuery(subDomain));

        return result.Match(
            list => Ok(list.Select(finance => new FinanceResponse(
                finance.FinanceId,
                finance.SiteId,
                finance.UserId,
                finance.SaleId,
                finance.TotalRevenue,
                finance.Description))),
            Problem);
    }
}
