using Inventory.Grpc.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Grpc.AspNetCore;

public static class GrpcModule
{
    public static void AddGrpcModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        var productServiceUrl = configuration["Grpc:ProductServiceUrl"];

        services.AddGrpcClient<ProductService.ProductServiceClient>(options =>
        {
            options.Address = new Uri(productServiceUrl);
        });

        services.AddScoped<IProductService, ProductServiceImplement>();
    }
}
