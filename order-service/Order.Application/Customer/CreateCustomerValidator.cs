using Order.Application.Abstractions;
using Order.Application.Contracts.Customer;
using FluentValidation;
using Order.Application.Contracts;

namespace Order.Application.Customer;

public class CreateCustomerValidator
{
    public class CustomerValidator : BaseValidator<CreateCustomer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.GivenName)
                .NotNull()
                .MaximumLength(255)
                .WithErrorCode(ErrorCodes.GIVEN_NAME_INVALID.ToString());
            
            RuleFor(x => x.FamilyName)
                .NotNull()
                .MaximumLength(255)
                .WithErrorCode(ErrorCodes.FAMILY_NAME_INVALID.ToString());
            
            RuleFor(x => x.Address)
                .NotNull()
                .MaximumLength(255)
                .WithErrorCode(ErrorCodes.ADRESS_INVALID.ToString());
        }
    }
}