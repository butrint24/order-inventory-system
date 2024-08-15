using FluentResults;
using Inventory.Application.Contracts.Abstractions;
using MediatR;

namespace Inventory.Application.Contracts.DeleteProduct;

public class DeleteProduct : ValidatorBase, IRequest<Result<DeleteProductResponse>>
{
    public Guid ProductId { get; set; }
}
