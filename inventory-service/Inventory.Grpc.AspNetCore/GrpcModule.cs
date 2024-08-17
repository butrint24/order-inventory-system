using Inventory.Grpc.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Grpc.AspNetCore;

public static class GrpcModule
{
    public static void AddGrpcModule(this IServiceCollection services)
    {
        services.AddGrpc();
    }
}
