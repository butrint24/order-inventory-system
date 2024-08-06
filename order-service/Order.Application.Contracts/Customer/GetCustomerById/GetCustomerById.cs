using FluentResults;
using MediatR;
using Order.Application.Contracts.Abstractions;

namespace Order.Application.Contracts.Customer.GetCustomerById;

public class GetCustomerById : ValidatorBase, IRequest<Result<GetCustomerByIdResponse>>
{
    public Guid CustomerId { get; set; }
}
