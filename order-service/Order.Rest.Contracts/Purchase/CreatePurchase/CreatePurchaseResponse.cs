using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.Purchase.CreatePurchase;

public class CreatePurchaseResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
