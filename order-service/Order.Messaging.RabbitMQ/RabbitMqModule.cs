using Microsoft.Extensions.DependencyInjection;
using Order.Messaging.Contracts;

namespace Order.Messaging.RabbitMQ;

public static class RabbitMqModule
{
    public static IServiceCollection AddRabbitMqModule(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBusClient, MessageBusClient>();
            
        return services;
    }
}
