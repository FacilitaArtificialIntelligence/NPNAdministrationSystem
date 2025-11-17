using MediatR;

namespace AdministrationSystem.Api.Controllers;

using AdministrationSystem.Application.Finances.Commands.CreateFinance;
using AdministrationSystem.Application.Finances.Commands.DeleteFinance;
using AdministrationSystem.Application.Finances.Queries.GetById;
using AdministrationSystem.Application.Finances.Queries.GetBySubDomain;
using AdministrationSystem.Contracts.Finance;


using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class FinanceController : ApiController
{
    private readonly ISender _mediator;

    public FinanceController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFinanceRequest request)
    {
        var command = new CreateFinanceCommand(
            request.SubDomain,
            request.UserId,
            request.SaleId,
            request.TotalRevenue,
            request.Description);

        var result = await _mediator.Send(command);

        return result.Match(
            f => Ok(result.Value),
            Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteFinanceCommand(id));
        return result.Match(_ => NoContent(), Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetFinanceByIdQuery(id));

        return result.Match(
            f => Ok(result.Value),
            Problem);
    }

    [HttpGet("BySubDomain/{subDomain:guid}")]
    public async Task<IActionResult> GetBySubDomain(Guid subDomain)
    {
        var result = await _mediator.Send(new GetFinanceBySubDomainQuery(subDomain));

        return result.Match(
            list => Ok(result.Value),
            Problem);
    }
}
