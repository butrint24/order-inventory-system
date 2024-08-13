using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Contracts;
using Order.Application.Contracts.Order.CreateOrder;
using Order.Application.Contracts.Order.DeleteOrder;
using Order.Application.Contracts.Order.GetOrderById;
using Order.Application.Contracts.Order.GetOrders;
using Order.Rest.Contracts.Purchase.CreatePurchase;
using Order.Rest.Contracts.Purchase.GetPurchaseByIdResponse;
using Order.Rest.Contracts.Purchase.GetPurchases;

namespace Order.Rest.AspNetCore;

[ApiController]
[Route("[controller]")]
public class PurchaseController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("/createPurchase")]
    public async Task<ActionResult<CreatePurchaseResponse>> CreateUserAsync(CreatePurchaseRequest request)
    {
        var command = mapper.Map<CreateOrder>(request);
        
        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<CreatePurchaseResponse>(result.Value);

        return Ok(response);
    }
    
    [HttpGet("/getPurchases")]
    public async Task<ActionResult<GetPurchasesResponse>> GetUsersAsync()
    {
        var result = await mediator.Send(new GetOrders());

        var response = mapper.Map<GetPurchasesResponse>(result.Value);
        
        return Ok(response);
    }
    
    [HttpGet("/getPurchase/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetPurchasesResponse>> GetPurchaseByIdAsync([FromRoute] Guid id)
    {
        var command = new GetOrderById
        {
            OrderId = id
        };
        
        var result = await mediator.Send(command);

        if (result.IsFailed && result.HasError(x => x.Message == ErrorCodes.ORDER_NOT_FOUND.ToString()))
            return NotFound();

        var response = mapper.Map<GetPurchaseByIdResponse>(result.Value);

        return Ok(response);
    }
    
    [HttpDelete("/deletePurchase/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteOrder
        {
            OrderId = id
        };
        
        var result = await mediator.Send(command);

        if (result.IsFailed)
            return NotFound();

        return NoContent();
    }
}
