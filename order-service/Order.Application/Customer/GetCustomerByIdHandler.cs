using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Contracts.Customer.GetCustomerById;
using Order.Data.Contracts;
using static FluentResults.Result;

namespace Order.Application.Customer;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, Result<GetCustomerByIdResponse>>
{
    private readonly IValidator<GetCustomerById> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public GetCustomerByIdHandler(
        IValidator<GetCustomerById> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);

        var entity = _mapper.Map<Data.Contracts.Customer>(request);

        var customer = await _repository.GetCustomerById(entity.CustomerId);

        if (customer == null)
            return new Result().WithError(ErrorCodes.CUSTOMER_NOT_FOUND.ToString());

        var customerOrders = await _repository.GetCustomerOrders(customer.CustomerId);

        var ordersDictionary = customerOrders
            .Where(order => order.CustomerId == customer.CustomerId)
            .DistinctBy(order => order.OrderId)
            .ToDictionary(order => order.OrderId);

        customer.Orders = ordersDictionary.Values.ToList();

        var result = _mapper.Map<GetCustomerByIdResponse>(customer);

        return result == null 
            ? new Result().WithError(ErrorCodes.CUSTOMER_NOT_FOUND.ToString()) 
            : Ok(result);
    }
}
