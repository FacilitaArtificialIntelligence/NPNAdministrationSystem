using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdministrationSystem.Contracts.Sites;
using AdministrationSystem.Application.Sites.Commands.CreateSite;
using AdministrationSystem.Application.Sites.Commands.UpdateSite;
using AdministrationSystem.Application.Sites.Commands.DeleteSite;
using AdministrationSystem.Application.Sites.Queries.GetSiteById;
using AdministrationSystem.Application.Sites.Queries.GetSitesByWebSiteId;

namespace AdministrationSystem.Api.Controllers;

[Route("api/[controller]")]
public class SitesController : ApiController
{
    private readonly ISender _mediator;

    public SitesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSiteRequest request)
    {
        var command = new CreateSiteCommand(
            request.WebSiteId,
            request.Name,
            request.SubDomain,
            request.Email,
            request.Description
        );

        var result = await _mediator.Send(command);

        return result.Match(
            site => Ok(site),
            Problem
        );
    }

    [HttpGet("{siteId:guid}")]
    public async Task<IActionResult> GetById(Guid siteId)
    {
        var query = new GetSiteByIdQuery(siteId);
        var result = await _mediator.Send(query);

        return result.Match(
            site => Ok(site),
            Problem
        );
    }

    [HttpGet("ByWebSite/{webSiteId:guid}")]
    public async Task<IActionResult> GetByWebSite(Guid webSiteId)
    {
        var query = new GetSitesByWebSiteIdQuery(webSiteId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPut("{siteId:guid}")]
    public async Task<IActionResult> Update(Guid siteId, UpdateSiteRequest request)
    {
        var command = new UpdateSiteCommand(
            siteId,
            request.Name,
            request.SubDomain,
            request.Email,
            request.Description
        );

        var result = await _mediator.Send(command);

        return result.Match(
            site => Ok(site),
            Problem
        );
    }

    [HttpDelete("{siteId:guid}")]
    public async Task<IActionResult> Delete(Guid siteId)
    {
        var command = new DeleteSiteCommand(siteId);

        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(),
            Problem
        );
    }
}
