using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Contracts;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Application.Contracts.Customer.DeleteCustomer;
using Order.Application.Contracts.Customer.GetCustomerById;
using Order.Application.Contracts.Customer.GetCustomers;
using Order.Rest.Contracts.User.CreateUser;
using Order.Rest.Contracts.User.GetUserById;
using Order.Rest.Contracts.User.GetUsers;

namespace Order.Rest.AspNetCore;

[ApiController]
[Route("api/v1")]
public class UserController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("/createUser")]
    public async Task<ActionResult<CreateUserResponse>> CreateUserAsync(CreateUserRequest request)
    {
        var command = mapper.Map<CreateCustomer>(request);
        
        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<CreateUserResponse>(result.Value);

        return Ok(response);
    }
    
    [HttpGet("/getUsers")]
    public async Task<ActionResult<GetUsersResponse>> GetUsersAsync()
    {
        var result = await mediator.Send(new GetCustomers());

        var response = mapper.Map<GetUsersResponse>(result.Value);
        
        return Ok(response);
    }
    
    [HttpGet("/getUser/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetUsersResponse>> GetUsersByIdAsync([FromRoute] Guid id)
    {
        var command = new GetCustomerById
        {
            CustomerId = id
        };
        
        var result = await mediator.Send(command);

        if (result.IsFailed && result.HasError(x => x.Message == ErrorCodes.CUSTOMER_NOT_FOUND.ToString()))
            return NotFound();

        var response = mapper.Map<GetUserByIdResponse>(result.Value);

        return Ok(response);
    }
    
    [HttpDelete("/deleteUser/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteCustomer
        {
            CustomerId = id
        };
        
        var result = await mediator.Send(command);

        if (result.IsFailed)
            return NotFound();

        return NoContent();
    }
}
