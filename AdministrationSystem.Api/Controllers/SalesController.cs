
namespace AdministrationSystem.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdministrationSystem.Application.Sales.Commands.Create;
using AdministrationSystem.Application.Sales.Commands.Delete;
using AdministrationSystem.Application.Sales.Queries.GetById;
using AdministrationSystem.Application.Sales.Queries.GetByProduct;
using AdministrationSystem.Contracts.Sales;


[Route("api/[controller]")]
public class SalesController : ApiController
{
    private readonly ISender _mediator;

    public SalesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleRequest request)
    {
        var command = new CreateSaleCommand(
            request.ProductId,
            request.SiteId,
            request.UserId,
            request.SaleDate,
            request.Amount);

        var result = await _mediator.Send(command);

        return result.Match(
            s => Ok(result.Value),
            Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteSaleCommand(id));
        return result.Match(_ => NoContent(), Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSaleByIdQuery(id));

        return result.Match(
            s => Ok(result.Value),
            Problem);
    }

    [HttpGet("ByProduct/{productId:guid}")]
    public async Task<IActionResult> GetByProduct(Guid productId)
    {
        var result = await _mediator.Send(new GetSalesByProductQuery(productId));

        return result.Match(
            list => Ok(result.Value),
            Problem);
    }
}
