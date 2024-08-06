using FluentResults;
using MediatR;
using Order.Application.Contracts.Abstractions;

namespace Order.Application.Contracts.Customer.DeleteCustomer;

public class DeleteCustomer : ValidatorBase, IRequest<Result<DeleteCustomerResponse>>
{
    public Guid CustomerId { get; set; }
}
