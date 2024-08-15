using FluentValidation;
using Inventory.Application.Abstractions;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.UpdateProduct;

namespace Inventory.Application.Product;

public class UpdateProductValidator : BaseValidator<UpdateProduct>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty)
            .NotNull()
            .WithErrorCode(ErrorCodes.PRODUCT_ID_INVALID.ToString());
        
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
