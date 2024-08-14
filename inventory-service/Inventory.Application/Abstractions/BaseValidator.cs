using FluentValidation;
using Inventory.Application.Contracts.Abstractions;

namespace Inventory.Application.Abstractions;

public abstract class BaseValidator<T> : AbstractValidator<T> where T: ValidatorBase
{
    public BaseValidator()
    {
        
    }
}
