using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.Purchase.GetPurchases;

public class PurchaseDetail : Abstractions.PurchaseDetail
{
    [JsonPropertyName("userInfo")]
    public UserInfo UserInfo { get; set; }
}
