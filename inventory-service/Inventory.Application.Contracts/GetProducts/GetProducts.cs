using FluentResults;
using MediatR;

namespace Inventory.Application.Contracts.GetProducts;

public record GetProducts : IRequest<Result<IList<Abstractions.Product>>>;
