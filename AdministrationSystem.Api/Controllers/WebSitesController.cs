using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdministrationSystem.Application.WebSites.Commands.CreateWebSite;
using AdministrationSystem.Application.WebSites.Commands.UpdateWebSite;
using AdministrationSystem.Application.WebSites.Commands.DeleteWebSite;
using AdministrationSystem.Application.WebSites.Queries.GetWebSiteById;
using AdministrationSystem.Application.WebSites.Queries.GetAllWebSites;
using Microsoft.AspNetCore.Authorization;
using AdministrationSystem.Contracts.WebSites;

namespace AdministrationSystem.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class WebSitesController : ApiController
{
    private readonly ISender _mediator;

    public WebSitesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWebSiteRequest request)
    {
        var command = new CreateWebSiteCommand(request.Url, request.Name, request.Email);

        var result = await _mediator.Send(command);

        return result.Match(
            ok => Ok(ok),
            Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetWebSiteByIdQuery(id);

        var result = await _mediator.Send(query);

        return result.Match(
            ok => Ok(ok),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllWebSitesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateWebSiteRequest request)
    {
        var command = new UpdateWebSiteCommand(id, request.Url, request.Name, request.Email);

        var result = await _mediator.Send(command);

        return result.Match(
            ok => Ok(ok),
            Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteWebSiteCommand(id);

        var result = await _mediator.Send(command);

        return result.Match(
            ok => Ok(),
            Problem);
    }
}
