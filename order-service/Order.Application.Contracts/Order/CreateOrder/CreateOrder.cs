using FluentResults;
using MediatR;
using Order.Application.Contracts.Abstractions;

namespace Order.Application.Contracts.Order.CreateOrder;

public class CreateOrder : ValidatorBase, IRequest<Result<CreateOrderResponse>>
{
    public string? Details { get; set; }

    public int Quantity { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal Price { get; set; }

    public Guid CustomerId { get; set; }

    public Abstractions.Customer CustomerDetails { get; set; }
}
