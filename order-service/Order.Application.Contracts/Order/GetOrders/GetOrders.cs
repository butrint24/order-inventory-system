using FluentResults;
using MediatR;

namespace Order.Application.Contracts.Order.GetOrders;

public record GetOrders : IRequest<Result<IList<Abstractions.Order>>>;
