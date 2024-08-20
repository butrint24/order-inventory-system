using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Order.Messaging.Contracts;
using RabbitMQ.Client;

namespace Order.Messaging.RabbitMQ;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;

        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]),
        };
        try
        {
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("--> Connected to Message Bus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not connect to the Message Bus {ex.Message}");
        }
    }
    
    public void PublishOrder(OrderCreated orderCreated)
    {
        var orderCreatedMessage = JsonSerializer.Serialize(orderCreated);
        
        if (_connection.IsOpen)
        {
            Console.WriteLine("--> RabbitMQ connection open, Sending Message...");
            SendMessage(orderCreatedMessage);
        }
        else
            Console.WriteLine("--> RabbitMQ connection is closed, not sending");
    }
    
    private void SendMessage(string orderCreated)
    {
        var body = Encoding.UTF8.GetBytes(orderCreated);

        _channel.BasicPublish(
            exchange: "trigger", 
            routingKey:"",
            basicProperties: null,
            body: body);
        Console.WriteLine($"--> We have sent {orderCreated}");
    }
    
    public void Dispose()
    {
        Console.WriteLine("--> MessageBus Disposed");
        if(_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
    
    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> RabbitMQ Connection Shutdown");
    }
}
