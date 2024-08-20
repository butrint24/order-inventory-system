using System.Net;
using AutoMapper;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Application.Contracts.DeleteProduct;
using Inventory.Application.Contracts.GetProductById;
using Inventory.Application.Contracts.GetProducts;
using Inventory.Application.Contracts.UpdateProduct;
using Inventory.Rest.Contracts.CreateItem;
using Inventory.Rest.Contracts.GetItemById;
using Inventory.Rest.Contracts.GetItems;
using Inventory.Rest.Contracts.UpdateItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Rest.AspNetCore;

[ApiController]
[Route("api/inventory/[controller]")]
public class ItemController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("createItem")]
    public async Task<ActionResult<CreateItemResponse>> CreateItemAsync(CreateItemRequest request)
    {
        var command = mapper.Map<CreateProduct>(request);
        
        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<CreateItemResponse>(result.Value);

        return Ok(response);
    }
    
    [HttpGet("getItems")]
    public async Task<ActionResult<GetItemsResponse>> GetItemsAsync()
    {
        var result = await mediator.Send(new GetProducts());

        var response = mapper.Map<GetItemsResponse>(result.Value);
        
        return Ok(response);
    }
    
    [HttpGet("getItem/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetItemByIdResponse>> GetItemsByIdAsync([FromRoute] Guid id)
    {
        var command = new GetProductById
        {
            ProductId = id
        };
        
        var result = await mediator.Send(command);

        if (result.IsFailed && result.HasError(x => x.Message == ErrorCodes.PRODUCT_NOT_FOUND.ToString()))
            return NotFound();

        var response = mapper.Map<GetItemByIdResponse>(result.Value);

        return Ok(response);
    }
    
    [HttpPut("updateItem/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<UpdateItemResponse>> UpdateItemAsync([FromRoute] Guid id, [FromBody] UpdateItemRequest request)
    {
        var command = mapper.Map<UpdateProduct>(request);

        command.ProductId = id;
        
        var result = await mediator.Send(command);

        if (result.IsFailed && result.HasError(x => x.Message == ErrorCodes.PRODUCT_NOT_FOUND.ToString()))
            return NotFound();
        
        if (result.IsFailed && result.HasError(x => x.Message == ErrorCodes.UPDATE_PRODUCT_FAILED.ToString()))
            return BadRequest();

        var response = mapper.Map<UpdateItemResponse>(result.Value);

        return Ok(response);
    }
    
    [HttpDelete("deleteItem/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteProduct
        {
            ProductId = id
        };
        
        var result = await mediator.Send(command);

        if (result.IsFailed)
            return NotFound();

        return NoContent();
    }
}
