using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Contracts.Customer;
using Order.Rest.Contracts.User;

namespace Order.Rest.AspNetCore;

[ApiController]
[Route("api/v1")]
public class UserController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("/createUser")]
    public async Task<ActionResult<CreateUserResponse>> CreatePersonAsync(CreateUserRequest request)
    {
        var command = mapper.Map<CreateCustomer>(request);
        
        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<CreateUserResponse>(result.Value);

        return Ok(response);
    }
}