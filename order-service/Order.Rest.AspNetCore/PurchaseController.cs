using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Contracts.Order.CreateOrder;
using Order.Application.Contracts.Order.GetOrders;
using Order.Rest.Contracts.Purchase.CreatePurchase;
using Order.Rest.Contracts.Purchase.GetPurchases;

namespace Order.Rest.AspNetCore;

[ApiController]
[Route("api/v1")]
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
}
