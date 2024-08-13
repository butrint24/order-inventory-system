using FluentValidation;
using Order.Application.Abstractions;
using Order.Application.Contracts;
using Order.Application.Contracts.Order.DeleteOrder;

namespace Order.Application.Order;

public class DeleteOrderValidator
{
    public class OrderValidator : BaseValidator<DeleteOrder>
    {
        public OrderValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .WithErrorCode(ErrorCodes.CUSTOMER_ID_INVALID.ToString());
        }
    }
}
