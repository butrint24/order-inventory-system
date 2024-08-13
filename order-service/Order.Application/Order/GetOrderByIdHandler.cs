using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Contracts.Order.GetOrderById;
using Order.Data.Contracts;
using static FluentResults.Result;

namespace Order.Application.Order;

public class GetOrderByIdHandler : IRequestHandler<GetOrderById, Result<GetOrderByIdResponse>>
{
    private readonly IValidator<GetOrderById> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public GetOrderByIdHandler(
        IValidator<GetOrderById> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetOrderByIdResponse>> Handle(GetOrderById request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);

        var entity = _mapper.Map<Data.Contracts.Order>(request);

        var order = await _repository.GetOrderById(entity.OrderId);

        if (order == null)
            return new Result().WithError(ErrorCodes.ORDER_NOT_FOUND.ToString());
        
        var result = _mapper.Map<GetOrderByIdResponse>(order);

        await CustomerDetails(result);

        return result == null 
            ? new Result().WithError(ErrorCodes.ORDER_NOT_FOUND.ToString()) 
            : Ok(result);
    }

    private async Task CustomerDetails(Contracts.Abstractions.Order request)
    {
        var customerDetails = await _repository.GetCustomerDetails(request.CustomerId);
        
        var result = _mapper.Map<Contracts.Abstractions.Customer>(customerDetails);

        request.CustomerDetails = new Contracts.Abstractions.Customer
        {
            CustomerId = result.CustomerId,
            Address = result.Address,
            FamilyName = result.FamilyName,
            GivenName = result.GivenName,
        };
    }
}
