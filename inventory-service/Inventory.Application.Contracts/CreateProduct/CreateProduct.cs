using FluentResults;
using Inventory.Application.Contracts.Abstractions;
using MediatR;

namespace Inventory.Application.Contracts.CreateProduct;

public class CreateProduct : ValidatorBase, IRequest<Result<CreateProductResponse>>
{
    public string Name { get; set; }

    public decimal Price { get; set; }
    
    public int Stock { get; set; }

    public string? Details { get; set; }
}
