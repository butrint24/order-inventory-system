using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Application.Contracts.Customer.GetCustomers;
using Order.Rest.Contracts.User.CreateUser;
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
}
