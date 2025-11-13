using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdministrationSystem.Application.Products.Commands.CreateProduct;
using AdministrationSystem.Application.Products.Commands.UpdateProduct;
using AdministrationSystem.Application.Products.Commands.DeleteProduct;
using AdministrationSystem.Application.Products.Queries.GetById;
using AdministrationSystem.Application.Products.Queries.GetBySite;
using AdministrationSystem.Contracts.Products;

namespace AdministrationSystem.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController : ApiController
{
    private readonly ISender _mediator;

    public ProductsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var command = new CreateProductCommand(
            request.SiteId,
            request.Name,
            request.Description,
            request.Price);

        var result = await _mediator.Send(command);

        return result.Match(
            p => Ok(p),
            Problem);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest request)
    {
        var command = new UpdateProductCommand(
            id,
            request.Name,
            request.Description,
            request.Price);

        var result = await _mediator.Send(command);

        return result.Match(
            p => Ok(p),
            Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        return result.Match(_ => NoContent(), Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return result.Match(
            p => Ok(p),
            Problem);
    }

    [HttpGet("BySite/{siteId:guid}")]
    public async Task<IActionResult> GetBySite(Guid siteId)
    {
        var result = await _mediator.Send(new GetProductsBySiteQuery(siteId));
        return result.Match(
            list => Ok(result.Value),
            Problem);
    }
}
