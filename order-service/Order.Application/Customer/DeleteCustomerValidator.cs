using FluentValidation;
using Order.Application.Abstractions;
using Order.Application.Contracts;
using Order.Application.Contracts.Customer.DeleteCustomer;

namespace Order.Application.Customer;

public class DeleteCustomerValidator
{
    public class CustomerValidator : BaseValidator<DeleteCustomer>
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
