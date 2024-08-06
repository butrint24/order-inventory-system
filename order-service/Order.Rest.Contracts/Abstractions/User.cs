using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.Abstractions;

public class User
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("orderDetail")]
    public List<OrderDetail> OrderDetail { get; set; } = [];
}
