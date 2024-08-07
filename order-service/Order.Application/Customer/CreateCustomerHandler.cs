using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Data.Contracts;
using static FluentResults.Result;

namespace Order.Application.Customer;

public class CreateCustomerHandler : IRequestHandler<CreateCustomer, Result<CreateCustomerResponse>>
{
    private readonly IValidator<CreateCustomer> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(
        IValidator<CreateCustomer> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomer request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);

        var mappedCustomer = _mapper.Map<Data.Contracts.Customer>(request);

        var customer = await _repository.CreateCustomerAsync(mappedCustomer);

        if (customer.ToResult().IsFailed)
            return new Result().WithError(ErrorCodes.CREATE_CUSTOMER_FAILED.ToString());

        var result = _mapper.Map<CreateCustomerResponse>(customer);

        return Ok(result);
    }
}
