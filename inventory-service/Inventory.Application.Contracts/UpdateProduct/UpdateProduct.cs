using FluentResults;
using Inventory.Application.Contracts.Abstractions;
using MediatR;

namespace Inventory.Application.Contracts.UpdateProduct;

public class UpdateProduct : ValidatorBase, IRequest<Result<UpdateProductResponse>>
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
    
    public int Stock { get; set; }

    public string? Details { get; set; }
}
