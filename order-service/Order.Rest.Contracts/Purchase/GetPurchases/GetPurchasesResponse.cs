using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.Purchase.GetPurchases;

public class GetPurchasesResponse
{
    [JsonPropertyName("purchases")]
    public IList<PurchaseDetail> Purchases { get; set; }
}
