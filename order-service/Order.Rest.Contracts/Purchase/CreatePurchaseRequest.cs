using System.Text.Json.Serialization;
using Order.Rest.Contracts.Abstractions;

namespace Order.Rest.Contracts.Product;

public class CreatePurchaseRequest : PurchaseDetail
{
    [JsonIgnore]
    public new Guid? Id { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; }
}
