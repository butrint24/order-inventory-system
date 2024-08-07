using FluentValidation;
using Order.Application.Abstractions;
using Order.Application.Contracts;
using Order.Application.Contracts.Order.CreateOrder;

namespace Order.Application.Order;

public class CreateOrderValidator
{
    public class OrderValidator : BaseValidator<CreateOrder>
    {
        public OrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .WithErrorCode(ErrorCodes.CUSTOMER_ID_INVALID.ToString());
            
            RuleFor(x => x.Details)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .WithErrorCode(ErrorCodes.ORDER_DETAILS_INVALID.ToString());
        }
    }
}
