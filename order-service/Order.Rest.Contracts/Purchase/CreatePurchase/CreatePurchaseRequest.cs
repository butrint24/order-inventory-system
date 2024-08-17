using System.Text.Json.Serialization;
using Order.Rest.Contracts.Abstractions;

namespace Order.Rest.Contracts.Purchase.CreatePurchase;

public class CreatePurchaseRequest : PurchaseDetail
{
    [JsonIgnore]
    public new Guid? Id { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; }
    
    [JsonIgnore]
    public new decimal Price { get; set; }
}
