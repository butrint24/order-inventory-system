namespace Order.Messaging.Contracts;

public interface IMessageBusClient
{
    void PublishOrder(PublishOrder publishOrder);
}
