using FluentValidation;
using Order.Application.Abstractions;
using Order.Application.Contracts;
using Order.Application.Contracts.Customer.GetCustomerById;

namespace Order.Application.Customer;

public class GetCustomerByIdValidator
{
    public class CustomerValidator : BaseValidator<GetCustomerById>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .WithErrorCode(ErrorCodes.CUSTOMER_ID_INVALID.ToString());
        }
    }
}
