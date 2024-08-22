using System.Text.Json;
using Inventory.Data.Contracts;
using Inventory.Messaging.Contracts;
using Microsoft.Extensions.DependencyInjection;
using static Inventory.Messaging.Contracts.Event;

namespace Inventory.Messaging.RabbitMQ;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;
    
    public void ProcessEvent(string eventMessage)
    {
        var eventType = DetermineEvent(eventMessage);

        switch (eventType)
        {
            case Order_Created:
                OrderCreatedEvent(eventMessage);
                break;
            case Order_Deleted:
                OrderDeletedEvent(eventMessage);
                break;
            default:
                break;
        }
    }

    private Event DetermineEvent(string notificationMessage)
    {
        Console.WriteLine("--> Determining Event!");

        var eventType = JsonSerializer.Deserialize<GenericEvent>(notificationMessage);

        if (Enum.TryParse<Event>(eventType.Event, out var parsedEvent))
        {
            switch (parsedEvent)
            {
                case Order_Created:
                    Console.WriteLine("Order Created Event Detected");
                    return Order_Created;

                case Order_Deleted:
                    Console.WriteLine("Order Deleted Event Detected");
                    return Order_Deleted;

                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return Undetermined;
            }
        }

        Console.WriteLine("--> Invalid event type in the message");
        return Undetermined;
    }
    
    private async void OrderCreatedEvent(string orderCreatedMessage)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IDataRepository>();

            var orderCreated = JsonSerializer.Deserialize<OrderCreated>(orderCreatedMessage);

            try
            {
                var product = await repo.GetProductById(orderCreated.ProductId);

                if (product == null)
                    throw new Exception();

                product.Stock -= orderCreated.Quantity;

                await repo.UpdateProductAsync(product);
                
            }
            catch (Exception ex)
            {

                Console.WriteLine($"--> Could not update stock {ex.Message}");
            }
        }
    }
    
    private async void OrderDeletedEvent(string orderDeletedMessage)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IDataRepository>();

            var orderDeleted = JsonSerializer.Deserialize<OrderCreated>(orderDeletedMessage);

            try
            {
                var product = await repo.GetProductById(orderDeleted.ProductId);

                if (product == null)
                    throw new Exception();

                product.Stock += orderDeleted.Quantity;

                await repo.UpdateProductAsync(product);
                
            }
            catch (Exception ex)
            {

                Console.WriteLine($"--> Could not update stock {ex.Message}");
            }
        }
    }
}
