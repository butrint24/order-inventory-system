using System.Text.Json.Serialization;

namespace Inventory.Rest.Contracts.Abstractions;

public class Item
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }
}
