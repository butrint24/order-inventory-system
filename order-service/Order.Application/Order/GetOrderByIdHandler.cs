using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Contracts.Order.GetOrderById;
using Order.Data.Contracts;
using Order.Grpc.Contracts;
using static FluentResults.Result;
using Product = Order.Application.Contracts.Abstractions.ProductInfo;

namespace Order.Application.Order;

public class GetOrderByIdHandler : IRequestHandler<GetOrderById, Result<GetOrderByIdResponse>>
{
    private readonly IValidator<GetOrderById> _validator;
    private readonly IDataRepository _repository;
    private readonly IProductDataClient _productDataClient;
    private readonly IMapper _mapper;

    public GetOrderByIdHandler(
        IValidator<GetOrderById> validator,
        IDataRepository repository,
        IProductDataClient productDataClient,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _productDataClient = productDataClient;
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

        var getProductDetails = _productDataClient.GetProduct(order.ProductId.ToString());

        if (getProductDetails == null)
            return new Result().WithError(ErrorCodes.PRODUCT_NOT_FOUND.ToString());
        
        var result = _mapper.Map<GetOrderByIdResponse>(order);

        result.ProductInfo = new Product
        {
            Name = getProductDetails.Name,
            Details = getProductDetails.Details,
        };

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
