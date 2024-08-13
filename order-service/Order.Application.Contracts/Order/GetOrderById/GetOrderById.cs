using FluentResults;
using MediatR;
using Order.Application.Contracts.Abstractions;

namespace Order.Application.Contracts.Order.GetOrderById;

public class GetOrderById : ValidatorBase, IRequest<Result<GetOrderByIdResponse>>
{
    public Guid OrderId { get; set; }
}
