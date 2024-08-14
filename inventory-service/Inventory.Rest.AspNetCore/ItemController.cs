using AutoMapper;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Rest.Contracts.CreateItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Rest.AspNetCore;

[ApiController]
[Route("api/v1")]
public class ItemController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("/createItem")]
    public async Task<ActionResult<CreateItemResponse>> CreateUserAsync(CreateItemRequest request)
    {
        var command = mapper.Map<CreateProduct>(request);
        
        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<CreateItemResponse>(result.Value);

        return Ok(response);
    }
}
