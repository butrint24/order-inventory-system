using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Contracts.Customer.DeleteCustomer;
using Order.Data.Contracts;
using static FluentResults.Result;

namespace Order.Application.Customer;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomer, Result<DeleteCustomerResponse>>
{
    private readonly IValidator<DeleteCustomer> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public DeleteCustomerHandler(
        IValidator<DeleteCustomer> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<DeleteCustomerResponse>> Handle(DeleteCustomer request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);
        
        var entity = _mapper.Map<Data.Contracts.Customer>(request);

        var getCustomer = await _repository.GetCustomerById(entity.CustomerId);

        if (getCustomer == null)
            return new Result().WithError(ErrorCodes.CUSTOMER_NOT_FOUND.ToString());

        await _repository.DeleteCustomerAsync(getCustomer);

        var result = _mapper.Map<DeleteCustomerResponse>(getCustomer);
        
        return Ok(result);
    }
}
