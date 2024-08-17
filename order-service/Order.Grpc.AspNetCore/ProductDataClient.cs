using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Inventory.Grpc.Contracts;
using Microsoft.Extensions.Configuration;
using Order.Grpc.Contracts;

namespace Order.Grpc.AspNetCore;

public class ProductDataClient : IProductDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public ProductDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }
    

    public Product GetProduct(string productId)
    {
        Console.WriteLine($"--> Calling GRPC service{_configuration["GrpcProduct"]}");
        var channel = GrpcChannel.ForAddress(_configuration["GrpcProduct"], new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new ProductService.ProductServiceClient(channel);
        var request = new GetProductRequest
        {
            ProductId = productId
        };
        
        try
        {
            var reply = client.GetProduct(request);
            return _mapper.Map<Product>(reply.Product);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not call GRPC server {ex.Message}");
            return null;
        }
    }
}
