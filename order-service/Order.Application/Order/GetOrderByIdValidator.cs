using FluentValidation;
using Order.Application.Abstractions;
using Order.Application.Contracts;
using Order.Application.Contracts.Order.GetOrderById;

namespace Order.Application.Order;

public class GetOrderByIdValidator
{
    public class OrderValidator : BaseValidator<GetOrderById>
    {
        public OrderValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .WithErrorCode(ErrorCodes.ORDER_ID_INVALID.ToString());
        }
    }
}
