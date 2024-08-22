namespace Inventory.Messaging.Contracts;

public interface IEventProcessor
{
    void ProcessEvent(string eventMessage);
}
