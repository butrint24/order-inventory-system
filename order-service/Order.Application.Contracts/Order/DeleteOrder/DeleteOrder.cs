using FluentResults;
using MediatR;
using Order.Application.Contracts.Abstractions;

namespace Order.Application.Contracts.Order.DeleteOrder;

public class DeleteOrder : ValidatorBase, IRequest<Result<DeleteOrderResponse>>
{
    public Guid OrderId { get; set; }
}
