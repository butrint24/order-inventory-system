using Inventory.Messaging.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Messaging.RabbitMQ;

public static class RabbitMqModule
{
    public static IServiceCollection AddRabbitMqModule(this IServiceCollection services)
    {
        services.AddHostedService<MessageBusSubscriber>();
        services.AddSingleton<IEventProcessor, EventProcessor>();
            
        return services;
    }
}
