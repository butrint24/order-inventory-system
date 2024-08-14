using FluentResults;
using MediatR;
using Order.Application.Contracts.Abstractions;

namespace Order.Application.Contracts.Customer.CreateCustomer;

public class CreateCustomer : ValidatorBase, IRequest<Result<CreateCustomerResponse>>
{
    public string GivenName { get; set; }

    public string FamilyName { get; set; }

    public string Address { get; set; }
}
