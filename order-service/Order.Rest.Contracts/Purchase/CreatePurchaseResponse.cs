using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.Product;

public class CreatePurchaseResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
