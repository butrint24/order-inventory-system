using FluentValidation;
using Inventory.Application.Abstractions;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.DeleteProduct;

namespace Inventory.Application.Product;

public class DeleteProductValidator : BaseValidator<DeleteProduct>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty)
            .NotNull()
            .WithErrorCode(ErrorCodes.PRODUCT_ID_INVALID.ToString());
    }
}
