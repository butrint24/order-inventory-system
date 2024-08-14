using FluentResults;
using Inventory.Application.Contracts.Abstractions;
using MediatR;

namespace Inventory.Application.Contracts.GetProductById;

public class GetProductById : ValidatorBase, IRequest<Result<GetProductByIdResponse>>
{
    public Guid ProductId { get; set; }
}
