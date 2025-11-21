using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdministrationSystem.Application.Sales.Commands.Create;
using AdministrationSystem.Application.Sales.Commands.Delete;
using AdministrationSystem.Application.Sales.Queries.GetById;
using AdministrationSystem.Application.Sales.Queries.GetByProduct;
using AdministrationSystem.Contracts.Sales;

namespace AdministrationSystem.Api.Controllers;

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
            sale => CreatedAtAction(
                nameof(GetById),
                new { id = sale.SaleId },
                new SaleResponse(
                    sale.SaleId,
                    sale.ProductId,
                    sale.SiteId,
                    sale.UserId,
                    sale.SaleDate,
                    sale.Amount
                )),
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
            sale => Ok(new SaleResponse(
                sale.SaleId,
                sale.ProductId,
                sale.SiteId,
                sale.UserId,
                sale.SaleDate,
                sale.Amount
            )),
            Problem);
    }

    [HttpGet("ByProduct/{productId:guid}")]
    public async Task<IActionResult> GetByProduct(Guid productId)
    {
        var result = await _mediator.Send(new GetSalesByProductQuery(productId));

        return result.Match(
            list => Ok(list.Select(sale => new SaleResponse(
                sale.SaleId,
                sale.ProductId,
                sale.SiteId,
                sale.UserId,
                sale.SaleDate,
                sale.Amount
            ))),
            Problem);
    }
}
