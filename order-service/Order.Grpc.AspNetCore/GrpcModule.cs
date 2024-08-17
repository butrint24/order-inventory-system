using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Order.Grpc.Contracts;

namespace Order.Grpc.AspNetCore;

public static class GrpcModule
{
    public static IServiceCollection AddGrpcModule(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly()).AddTransient<MappingRegister>();
        
        services.AddScoped<IProductDataClient, ProductDataClient>();
        
        return services;
    }
}
