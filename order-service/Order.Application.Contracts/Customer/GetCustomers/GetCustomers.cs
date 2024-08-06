using FluentResults;
using MediatR;

namespace Order.Application.Contracts.Customer.GetCustomers;

public record GetCustomers : IRequest<Result<IList<Abstractions.Customer>>>;