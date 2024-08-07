using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Application.Contracts.Order.CreateOrder;
using Order.Data.Contracts;
using static FluentResults.Result;

namespace Order.Application.Order;

public class CreateOrderHandler : IRequestHandler<CreateOrder, Result<CreateOrderResponse>>
{
    private readonly IValidator<CreateOrder> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public CreateOrderHandler(
        IValidator<CreateOrder> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<CreateOrderResponse>> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);

        var mappedOrder = _mapper.Map<Data.Contracts.Order>(request);

        var verifyUser = await _repository.GetCustomerById(request.CustomerId);
        
        if (verifyUser == null || verifyUser.CustomerId == Guid.Empty)
            return new Result().WithError(ErrorCodes.CUSTOMER_NOT_FOUND.ToString());
        
        var order = await _repository.CreateOrderAsync(mappedOrder);

        if (order.ToResult().IsFailed)
            return new Result().WithError(ErrorCodes.ORDER_CREATION_FAILED.ToString());

        var result = _mapper.Map<CreateOrderResponse>(order);

        return Ok(result);
    }
}
