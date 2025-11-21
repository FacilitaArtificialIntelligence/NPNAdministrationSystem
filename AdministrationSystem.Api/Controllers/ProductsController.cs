using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdministrationSystem.Application.Products.Commands.CreateProduct;
using AdministrationSystem.Application.Products.Commands.UpdateProduct;
using AdministrationSystem.Application.Products.Commands.DeleteProduct;
using AdministrationSystem.Application.Products.Queries.GetById;
using AdministrationSystem.Application.Products.Queries.GetBySite;
using AdministrationSystem.Contracts.Products;
using Microsoft.AspNetCore.Authorization;

namespace AdministrationSystem.Api.Controllers;

[Authorize]
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
            product => CreatedAtAction(
                nameof(GetById),
                new { id = product.ProductId },
                new ProductResponse(
                    product.ProductId,
                    product.SiteId,
                    product.Name,
                    product.Description,
                    product.Price
                )),
            Problem);
    }

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> Update(Guid productId, UpdateProductRequest request)
    {
        var command = new UpdateProductCommand(
            productId,
            request.Name,
            request.Description,
            request.Price);

        var result = await _mediator.Send(command);
        return result.Match(_ => NoContent(), Problem);
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        var result = await _mediator.Send(new DeleteProductCommand(productId));
        return result.Match(_ => NoContent(), Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return result.Match(
            product => Ok(new ProductResponse(
                product.ProductId,
                product.SiteId,
                product.Name,
                product.Description,
                product.Price
            )),
            Problem);
    }

    [HttpGet("BySite/{siteId:guid}")]
    public async Task<IActionResult> GetBySite(Guid siteId)
    {
        var result = await _mediator.Send(new GetProductsBySiteQuery(siteId));
        return result.Match(
            list => Ok(list.Select(product => new ProductResponse(
                product.ProductId,
                product.SiteId,
                product.Name,
                product.Description,
                product.Price
            ))),
            Problem);
    }
}
