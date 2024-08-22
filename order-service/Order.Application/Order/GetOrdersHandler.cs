using AutoMapper;
using FluentResults;
using MediatR;
using Order.Application.Contracts.Abstractions;
using Order.Application.Contracts.Order.GetOrders;
using Order.Data.Contracts;
using Order.Grpc.Contracts;
using static FluentResults.Result;

namespace Order.Application.Order;

public class GetOrdersHandler : IRequestHandler<GetOrders, Result<IList<Contracts.Abstractions.Order>>>
{
    private readonly IDataRepository _repository;
    private readonly IProductDataClient _productDataClient;
    private readonly IMapper _mapper;

    public GetOrdersHandler(
        IDataRepository repository,
        IProductDataClient productDataClient,
        IMapper mapper)
    {
        _repository = repository;
        _productDataClient = productDataClient;
        _mapper = mapper;
    }

    public async Task<Result<IList<Contracts.Abstractions.Order>>> Handle(GetOrders request, CancellationToken cancellationToken)
    {
        var getOrders = await _repository.GetAllOrders();

        foreach (var @order in getOrders)
        {
            var customerDetails = await _repository.GetCustomerDetails(@order.CustomerId);

            order.CustomerDetails = customerDetails;
        }

        var result = _mapper.Map<IList<Contracts.Abstractions.Order>>(getOrders);

        foreach (var @order in result)
        {
            var productDetails = _productDataClient.GetProduct(@order.ProductId.ToString());

            if (productDetails != null)
            {
                order.ProductInfo = new ProductInfo
                {
                    Name = productDetails.Name,
                    Details = productDetails.Details
                };
            }
        }
        
        return Ok(result);
    }
}
