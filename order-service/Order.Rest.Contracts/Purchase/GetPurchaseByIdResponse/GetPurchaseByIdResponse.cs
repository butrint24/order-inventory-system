using System.Text.Json.Serialization;
using Order.Rest.Contracts.Purchase.Common;

namespace Order.Rest.Contracts.Purchase.GetPurchaseByIdResponse;

public class GetPurchaseByIdResponse : Abstractions.PurchaseDetail
{
    [JsonPropertyName("userInfo")] 
    public UserInfo UserInfo { get; set; }
}
