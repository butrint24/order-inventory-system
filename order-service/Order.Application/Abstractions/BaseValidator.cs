using FluentValidation;
using Order.Application.Contracts.Abstractions;

namespace Order.Application.Abstractions;

public abstract class BaseValidator<T> : AbstractValidator<T> where T: ValidatorBase
{
    public BaseValidator()
    {
        
    }
}