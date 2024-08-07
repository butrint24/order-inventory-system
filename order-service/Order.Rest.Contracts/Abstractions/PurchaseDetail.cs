using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.Abstractions;

public class PurchaseDetail
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    [JsonPropertyName("status")]
    public PurchaseStatus Status { get; set; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}
