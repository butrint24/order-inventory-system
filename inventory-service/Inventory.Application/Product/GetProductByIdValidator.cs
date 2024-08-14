using FluentValidation;
using Inventory.Application.Abstractions;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.GetProductById;

namespace Inventory.Application.Product;

public class GetProductByIdValidator : BaseValidator<GetProductById>
{
    public GetProductByIdValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty)
            .NotNull()
            .WithErrorCode(ErrorCodes.PRODUCT_ID_INVALID.ToString());
    }
}
