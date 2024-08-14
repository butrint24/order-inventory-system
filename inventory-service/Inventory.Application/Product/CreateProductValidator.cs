using FluentValidation;
using Inventory.Application.Abstractions;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.CreateProduct;

namespace Inventory.Application.Product;

public class CreateProductValidator : BaseValidator<CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Stock)
            .GreaterThan(0)
            .WithErrorCode(ErrorCodes.INVALID_STOCK_NUMBER.ToString());
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCodes.INVALID_PRODUCT_NAME.ToString());
        
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithErrorCode(ErrorCodes.INVALID_PRICE.ToString());
    }
}
